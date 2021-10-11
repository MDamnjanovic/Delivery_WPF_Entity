using Dostava_WPFEntity.Baza;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dostava_WPFEntity
{
    /// <summary>
    /// Interaction logic for FrmPotvrdaBrisanje.xaml
    /// </summary>
    public partial class FrmPotvrdaBrisanje : Window
    {
        DostavaEntities context = new DostavaEntities();
        Object objekatBrisanje = null;
        public FrmPotvrdaBrisanje(Object objekatBrisanje)
        {
            InitializeComponent();
            this.objekatBrisanje = objekatBrisanje;
        }

        private void btnNe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDa_Click(object sender, RoutedEventArgs e)
        {

            if(objekatBrisanje is Artikal)
            {

                Artikal artikal = (Artikal)objekatBrisanje;

                List<dostavaArtikala> lst = new List<dostavaArtikala>();
                //dostavaArtikala dostavaArtikla = context.dostavaArtikalas.Where(da => da.id_artikal == artikal.id).FirstOrDefault();

                List<dostavaArtikala> lst2 = context.dostavaArtikalas.Where(da => da.id_artikal==artikal.id).ToList();


                foreach (dostavaArtikala da in context.dostavaArtikalas)
                   foreach(dostavaArtikala da2 in lst2) 
                    {
                        if (da.id_dostava == da2.id_dostava)
                            lst.Add(da);
                    }



                context.dostavaArtikalas.RemoveRange(context.dostavaArtikalas.Where(a => a.id_artikal == artikal.id));
                context.SaveChanges();

                foreach (dostavaArtikala da in lst)
                {
                    int idDostave = da.id_dostava;

                    int count = lst.Count(d => d.id_dostava == idDostave);
                    //MessageBox.Show(count.ToString());
                    if (count == 1)
                    {
                        context.Dostavas.RemoveRange(context.Dostavas.Where(d => d.id == idDostave));
                        context.SaveChanges();

                        Dostava dostavaBris = MainWindow.lstDostave.Where(d => d.id == idDostave).FirstOrDefault();
                        MainWindow.lstDostave.Remove(dostavaBris);

                    }
                }


                Artikal artBrisanje = context.Artikals.Where(a => a.id == artikal.id).FirstOrDefault();
                context.Artikals.Remove(artBrisanje);
                context.SaveChanges();


                MainWindow.lstArtikli.Remove(artikal);



            }
            if (objekatBrisanje is Dostava)
            {
                Dostava dostava = (Dostava)objekatBrisanje;
                context.dostavaArtikalas.RemoveRange(context.dostavaArtikalas.Where(d => d.id_dostava == dostava.id));
                context.SaveChanges();
                Dostava dostBrisanje = context.Dostavas.Where(d => d.id == dostava.id).FirstOrDefault();
                context.Dostavas.Remove(dostBrisanje);
                context.SaveChanges();

                MainWindow.lstDostave.Remove(dostava);
                
            }
            this.Close();
        }
    }
}
