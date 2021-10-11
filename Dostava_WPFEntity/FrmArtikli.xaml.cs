using Dostava_WPFEntity.Baza;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FrmArtikli.xaml
    /// </summary>
    /// 
    public enum Stanje{DODAJ, IZMENI};
    public partial class FrmArtikli : Window
    {

        Artikal artikal;
        Stanje stanje;
        DostavaEntities context = new DostavaEntities();
       
        public FrmArtikli(Artikal artikal, Stanje stanje=Stanje.DODAJ)
        {
            InitializeComponent();
            this.artikal = artikal;
            this.stanje = stanje;

            tbNaziv.DataContext = artikal;
            tbKolicina.DataContext = artikal;
            tbCena.DataContext = artikal; 

            if (stanje == Stanje.IZMENI)
                btnDodaj.Content = "Izmeni";
        }

        bool isDataValid ()
        {
            bool valid = false;
            if (tbNaziv.Text != "" && tbKolicina.Text != "" && tbCena.Text != "" && int.TryParse(tbKolicina.Text, out int kolicina) && double.TryParse(tbCena.Text, out double cena))
                valid = true;

            return valid;
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (isDataValid())
            {
                DialogResult = true;
                if (stanje == Stanje.DODAJ)
                {
                    int noviId = 0;
                    if (MainWindow.lstArtikli.Count > 0)
                        noviId = MainWindow.lstArtikli.Max(a => a.id);

                    noviId++;
                    artikal.id = noviId;
                    MainWindow.lstArtikli.Add(artikal);

                    context.Artikals.Add(artikal);
                    context.SaveChanges();


                }
                if (stanje == Stanje.IZMENI)
                {
                    Artikal art = context.Artikals.Where(a => a.id == this.artikal.id).FirstOrDefault();
                    if (art != null)
                    {
                        art.id = this.artikal.id;
                        art.naziv = this.artikal.naziv;
                        art.kolicina = this.artikal.kolicina;
                        art.cena = this.artikal.cena;
                        context.SaveChanges();
                    }
                }
                this.Close();
            }
            else
                MessageBox.Show("Niste popunili sva polja");
        }


        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
