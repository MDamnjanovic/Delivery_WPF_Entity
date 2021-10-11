using Dostava_WPFEntity.Baza;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for FrmDostava.xaml
    /// </summary>
    public partial class FrmDostava : Window
    {
        Dostava dostava;
        Stanje stanje;
        ICollectionView viewPrikaz;
        DostavaEntities context = new DostavaEntities();
        ObservableCollection<dostavaArtikala> dodatiArtikli = new ObservableCollection<dostavaArtikala>();

        public FrmDostava(Dostava dostava, Stanje stanje=Stanje.DODAJ)
        {
            InitializeComponent();
            this.dostava = dostava;
            this.stanje = stanje;
           

            tbAdresa.DataContext = dostava;
            dtDatum.DataContext = dostava;

            //if (stanje == Stanje.IZMENI)
            //{
            //    dtDatum.SelectedDate = Convert.ToDateTime(dostava.datum);
            //}

            inicijalizujPrikaz_artikli();

            if (stanje == Stanje.IZMENI)
                refresDodatiArtikli();

        }

        void inicijalizujPrikaz_artikli()
        {
            viewPrikaz = CollectionViewSource.GetDefaultView(MainWindow.lstArtikli);
            dgArtikli.ItemsSource = viewPrikaz;
            dgArtikli.IsSynchronizedWithCurrentItem = true;
            dgArtikli.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //lbArtikliPorudzbina.Items.Add(selArtikal.ToString() + " x " + tbKolicina.Text);
        void refresDodatiArtikli ()
        {
            lbArtikliPorudzbina.Items.Clear();
            if(this.dostava!=null)
            foreach(dostavaArtikala da in dostava.dostavaArtikalas)
            {
                Artikal art = context.Artikals.Where(a => a.id == da.id_artikal).FirstOrDefault();
                if(art!=null)
                    lbArtikliPorudzbina.Items.Add(art.ToString() + " x " + da.kolicina);
            }
        }

        private void btnDodajArtikal_Click(object sender, RoutedEventArgs e)
        {
            if (dgArtikli.SelectedIndex>-1 && tbKolicina.Text!="")
            {

                Artikal selArtikal = viewPrikaz.CurrentItem as Artikal;

                if (selArtikal.kolicina >= int.Parse(tbKolicina.Text))
                {

                    dostavaArtikala dodatArtikal = new dostavaArtikala(dostava.id, selArtikal.id, int.Parse(tbKolicina.Text), null, null);
                    bool postojiArtikal = this.dostava.dostavaArtikalas.Any(
                        a => a.id_artikal == dodatArtikal.id_artikal
                        );
                    if (postojiArtikal)
                    {
                        this.dostava.dostavaArtikalas.Where(a => a.id_artikal == dodatArtikal.id_artikal).FirstOrDefault().kolicina += dodatArtikal.kolicina;
                    }
                    else
                    {
                        this.dostava.dostavaArtikalas.Add(dodatArtikal);
                    }

                    refresDodatiArtikli();

                    int indeks = MainWindow.lstArtikli.IndexOf(selArtikal);
                    MainWindow.lstArtikli[indeks].kolicina-= int.Parse(tbKolicina.Text);
                    inicijalizujPrikaz_artikli();

                    Artikal artikalIzmKolicine = context.Artikals.Where(a=>a.id==selArtikal.id).FirstOrDefault();
                    if (artikalIzmKolicine != null)
                    {
                        artikalIzmKolicine.kolicina -= int.Parse(tbKolicina.Text);
                        context.SaveChanges();
                    }

                }




            }
        }

        private void btnPotvrdiPorudzbinu_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            if (stanje == Stanje.DODAJ)
            {
                MainWindow.lstDostave.Add(this.dostava);

                context.Dostavas.Add(this.dostava);
                context.SaveChanges();
            }

            this.Close();
        }
    }
}
