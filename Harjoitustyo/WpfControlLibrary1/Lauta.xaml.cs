using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pelialue
{

    public class Puolitus : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (double)value / 3;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (double)value * 3;
        }
    }
    public class Ruudunkoko : IValueConverter
    {
        /// <summary>
        /// otetaan 2/9 koko gridin leveydestä joka kertoo kahden ruudun leveyden tai korkeuden.
        /// </summary>
        /// <param name="value">arvo joka muunnetaan, tässä tapauksessa ruudukon leveys tai korkeus</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (2 * (double)value) / 9;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (double)value * 4.5;
        }
    }
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Lauta : UserControl
    {
        private Brush p1Vari = Brushes.Bisque;
        private Brush p2Vari = Brushes.Red;

        public Lauta()
        {
            AddHandler(PeliNappula.Nappula.nappialas, new RoutedEventHandler(Radiointi));
            poisto = false;


            InitializeComponent();

        }

        private void Radiointi(object sender, RoutedEventArgs e)
        {
            alue.hutiClick();
        }

        public bool plisaysmenossa;


        private List<PeliNappula.Nappula> p1Kaikki = new List<PeliNappula.Nappula>();
        private List<PeliNappula.Nappula> p2Kaikki = new List<PeliNappula.Nappula>();
        private bool pelaajan1Vuoro = true;
        private int lisaykset = 0;
        public bool poisto;

        private Label poistoHelper = new Label();


        private void Klikki(object sender, MouseButtonEventArgs e)
        {
            if (poisto) return;
            foreach (UIElement u in ruudukko.Children)
                if (u is Ellipse) ((Ellipse)u).Fill = Brushes.Black;
            PeliNappula.Nappula nappula;

            if (lisaykset == 18 && (nappula = getCheckedNappula()) != null)
            {
                if (p1Kaikki.Contains(nappula) && !pelaajan1Vuoro) { nappula.Uncheck(); return; }
                if (p2Kaikki.Contains(nappula) && pelaajan1Vuoro) { nappula.Uncheck(); return; }
                List<PeliNappula.Nappula> temp = new List<PeliNappula.Nappula>();
                Ellipse elli = (Ellipse)sender;
                List<Ellipse> naapurit = new List<Ellipse>();
                etsiNaapurit(nappula, elli, naapurit);

                if (!naapurit.Contains(elli)) { elli.Fill = Brushes.DarkRed; return; }
                ruudukko.Children.Remove(nappula);
                Grid.SetColumnSpan(nappula, Grid.GetColumnSpan(elli));

                Grid.SetRowSpan(nappula, Grid.GetRowSpan(elli));
                Grid.SetColumn(nappula, Grid.GetColumn(elli));

                Grid.SetRow(nappula, Grid.GetRow(elli));
                if (pelaajan1Vuoro) temp = p1Kaikki;
                else temp = p2Kaikki;
                if (tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), temp))
                {
                    poistoHelper.Content = "Poista nappula hiiren oikealla näppäimellä";
                    Grid.SetColumnSpan(poistoHelper, 9); ruudukko.Children.Add(poistoHelper); poisto = true;
                }

                ruudukko.Children.Add(nappula);
                pelaajan1Vuoro = !pelaajan1Vuoro;
                return;
            }


            if (plisaysmenossa)
            {
                if (lisaykset == 18) { plisaysmenossa = true; return; }
                nappula = new PeliNappula.Nappula();

                if (pelaajan1Vuoro) { nappula.Vari = p1Vari; p1Kaikki.Add(nappula); }
                else { p2Kaikki.Add(nappula); nappula.Vari = p2Vari; }
                lisaykset++;


                Ellipse elli = (Ellipse)sender;

                Grid.SetColumnSpan(nappula, Grid.GetColumnSpan(elli));

                Grid.SetRowSpan(nappula, Grid.GetRowSpan(elli));
                Grid.SetColumn(nappula, Grid.GetColumn(elli));

                Grid.SetRow(nappula, Grid.GetRow(elli));
                nappula.MouseRightButtonDown += poista;
                ruudukko.Children.Add(nappula);
                List<PeliNappula.Nappula> temp;
                if (pelaajan1Vuoro) temp = p1Kaikki;
                else temp = p2Kaikki;
                if (tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), temp))
                {
                    poistoHelper.Content = "Poista nappula hiiren oikealla näppäimellä";
                    Grid.SetColumnSpan(poistoHelper, 9); ruudukko.Children.Add(poistoHelper); poisto = true;
                }
                pelaajan1Vuoro = !pelaajan1Vuoro;
                plisaysmenossa = false;
                if (lisaykset == 18) plisaysmenossa = true;
            }
        }

        private void etsiNaapurit(PeliNappula.Nappula nappula, Ellipse elli, List<Ellipse> naapurit)
        {

        }

        private void poista(object sender, MouseButtonEventArgs e)
        {
            if (poisto)
            {
                PeliNappula.Nappula nappula = (PeliNappula.Nappula)sender;
                if (pelaajan1Vuoro && p1Kaikki.Contains(nappula))
                {
                    if (!tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), p1Kaikki))
                    {
                        ruudukko.Children.Remove(nappula);
                        ruudukko.Children.Remove(poistoHelper);
                        p1Kaikki.Remove(nappula);
                        poisto = false;
                    }
                }
                else if (!pelaajan1Vuoro && p2Kaikki.Contains(nappula))
                {
                    if (!tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), p2Kaikki))
                    {
                        ruudukko.Children.Remove(nappula);
                        ruudukko.Children.Remove(poistoHelper);
                        p2Kaikki.Remove(nappula);
                        poisto = false;
                    }
                }
            }

        }

        private bool tarkistaMylly(int rivi, int sarake, List<PeliNappula.Nappula> nappulat)
        {
            int omia = 0;
            if (rivi != 4)
            {
                foreach (PeliNappula.Nappula n in nappulat)
                {
                    if (Grid.GetRow(n) == rivi) omia++;
                }
                if (omia == 3) return true;
            }
            else
            {
                if (sarake < 5)
                    foreach (PeliNappula.Nappula n in nappulat)
                    {
                        if (Grid.GetRow(n) == rivi && Grid.GetColumn(n) < 5) omia++;
                    }
                else
                    foreach (PeliNappula.Nappula n in nappulat)
                    {
                        if (Grid.GetRow(n) == rivi && Grid.GetColumn(n) >= 5) omia++;
                    }
                if (omia == 3) return true;
            }
            omia = 0;

            if (sarake != 4)
            {
                foreach (PeliNappula.Nappula n in nappulat)
                    if (Grid.GetColumn(n) == sarake && sarake != 4) omia++;
            }
            else
            {
                if (rivi < 5)
                    foreach (PeliNappula.Nappula n in nappulat)
                    {
                        if (Grid.GetColumn(n) == sarake && Grid.GetRow(n) < 5) omia++;
                    }
                else
                    foreach (PeliNappula.Nappula n in nappulat)
                    {
                        if (Grid.GetColumn(n) == sarake && Grid.GetRow(n) >= 5) omia++;
                    }
            }

            return omia == 3;
        }

        private void alue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hutiClick();
        }

        public PeliNappula.Nappula getCheckedNappula()
        {
            foreach (UIElement nap in ruudukko.Children)
                if (nap is PeliNappula.Nappula)
                    if (((PeliNappula.Nappula)nap).getChecked()) return (PeliNappula.Nappula)nap;
            return null;
        }

        public void hutiClick()
        {
            foreach (UIElement nap in ruudukko.Children)
                if (nap is PeliNappula.Nappula) ((PeliNappula.Nappula)nap).Uncheck();
        }
    }
}
