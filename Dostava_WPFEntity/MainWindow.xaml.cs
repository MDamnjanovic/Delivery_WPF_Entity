using Dostava_WPFEntity.Baza;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dostava_WPFEntity
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ICollectionView viewPrikaz;

        DostavaEntities context = new DostavaEntities();
        public static ObservableCollection<Artikal> lstArtikli = new ObservableCollection<Artikal>();
        public static ObservableCollection<Dostava> lstDostave = new ObservableCollection<Dostava>();
        public static ObservableCollection<dostavaArtikala> lstDostaveArtikli = new ObservableCollection<dostavaArtikala>();

        //private static MainWindow instance = null;
        //public static MainWindow Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new MainWindow();
        //        return instance;
        //    }
        //}


        public MainWindow()
        {
            InitializeComponent();

            inicijalizujKolekcije();
            inicijalizujFormu();
            //testUpis();
        }

        void testUpis()
        {
            Artikal art = new Artikal(2, "cips", 10, 100);
            lstArtikli.Add(art);
            context.Artikals.Add(art);
            context.SaveChanges();
            MessageBox.Show(context.Artikals.Count().ToString());
        }

        void inicijalizujKolekcije()
        {
            foreach (Artikal a in context.Artikals)
            {
                lstArtikli.Add(a);
            }
            foreach (Dostava d in context.Dostavas)
            {
                lstDostave.Add(d);
            }
            foreach (dostavaArtikala da in context.dostavaArtikalas)
            {
                lstDostaveArtikli.Add(da);
            }
        }
       

        void inicijalizujFormu()
        {
            cbPrikaz.Items.Add("Artikli");
            cbPrikaz.Items.Add("Dostava");
            cbPrikaz.SelectedIndex = 0;
            dodeliDgPrikaz();
        }

        void dodeliDgPrikaz()
        {
            if (cbPrikaz.SelectedIndex == 0)
            {
                viewPrikaz = CollectionViewSource.GetDefaultView(lstArtikli);
                dgPrikaz.ItemsSource = viewPrikaz;
                dgPrikaz.IsSynchronizedWithCurrentItem = true;
                dgPrikaz.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            else if (cbPrikaz.SelectedIndex == 1)
            {
                viewPrikaz = CollectionViewSource.GetDefaultView(lstDostave);
                dgPrikaz.ItemsSource = viewPrikaz;
                dgPrikaz.IsSynchronizedWithCurrentItem = true;
                dgPrikaz.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void cbPrikaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dodeliDgPrikaz();
        }

        private void dgPrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "dostavaArtikalas")
                e.Cancel = true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (cbPrikaz.SelectedIndex == 0)
            {
                Artikal newArtikal = new Artikal();
                FrmArtikli artikal = new FrmArtikli(newArtikal);
                artikal.ShowDialog();
            }
            else if (cbPrikaz.SelectedIndex == 1)
            {

                int noviId = 0;
                if (lstDostave.Count > 0)
                    noviId = lstDostave.Max(d => d.id);
                noviId++;

                Dostava dostava = new Dostava();
                dostava.id = noviId;
                dostava.datum = DateTime.Now.ToString();
                FrmDostava frmDostava = new FrmDostava(dostava);
                frmDostava.ShowDialog();
            }
        }

        private void btnBrisi_Click(object sender, RoutedEventArgs e)
        {
            if (cbPrikaz.SelectedIndex == 0)
            {
                if (dgPrikaz.SelectedIndex > -1)
                {
                    Artikal selektovaniArtikal = viewPrikaz.CurrentItem as Artikal;

                    FrmPotvrdaBrisanje brisanje = new FrmPotvrdaBrisanje(selektovaniArtikal);
                    brisanje.ShowDialog();
                }
            }
            if(cbPrikaz.SelectedIndex == 1)
            {
                if (dgPrikaz.SelectedIndex > -1)
                {
                    Dostava selDostava = viewPrikaz.CurrentItem as Dostava;

                    FrmPotvrdaBrisanje brisanje = new FrmPotvrdaBrisanje(selDostava);
                    brisanje.ShowDialog();
                }
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if(cbPrikaz.SelectedIndex == 0)
            {
                if (dgPrikaz.SelectedIndex > -1)
                {
                    Artikal selektArtikal = viewPrikaz.CurrentItem as Artikal;
                    Artikal kopijaArt = new Artikal(selektArtikal);
                    FrmArtikli izmena = new FrmArtikli(selektArtikal, Stanje.IZMENI);
                    izmena.ShowDialog();

                    if (izmena.DialogResult != true)
                    {
                        int index = lstArtikli.IndexOf(selektArtikal);
                        lstArtikli[index] = kopijaArt;
                    }
                }
            }
            if (cbPrikaz.SelectedIndex == 1)
            {
                if (dgPrikaz.SelectedIndex > -1)
                {
                    Dostava selektDostava = viewPrikaz.CurrentItem as Dostava;
                    Dostava kopijaDostava = new Dostava(selektDostava);
                    FrmDostava izmenaDostava = new FrmDostava(selektDostava, Stanje.IZMENI);
                    izmenaDostava.ShowDialog();
                    if(izmenaDostava.DialogResult != true)
                    {
                        int index = lstDostave.IndexOf(selektDostava);
                        lstDostave[index] = kopijaDostava;
                    }
                }
            }
        }
    }
}
