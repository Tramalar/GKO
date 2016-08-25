using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
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
        public Brush p1Vari = Brushes.Bisque;
        public Brush p2Vari = Brushes.Red;

        public Lauta()
        {
            AddHandler(PeliNappula.Nappula.nappialas, new RoutedEventHandler(Radiointi));
            poisto = false;

            InitializeComponent();
            pelaajan1Vuoro = r.Next(2) == 1;
            Helper.Text = lisaysapu;
            Helper.TextWrapping = TextWrapping.WrapWithOverflow;
            Helper.Margin = new Thickness(5);

            asetaNaapurit();

        }

        public void varjaa(bool v,Brush vari)
        {
            if (v) { p1Vari = vari; foreach (PeliNappula.Nappula n in p1Nappulat) n.Vari = vari; }
            else { p2Vari = vari; foreach (PeliNappula.Nappula n in p2Nappulat) n.Vari = vari; }
        }

        private void asetaNaapurit()
        {
            NaapuriAttach.SetNaapuriOminaisuus(elli1, new List<Ellipse> { elli2, elli8 });
            NaapuriAttach.SetNaapuriOminaisuus(elli2, new List<Ellipse> { elli1, elli3, elli10 });
            NaapuriAttach.SetNaapuriOminaisuus(elli3, new List<Ellipse> { elli2, elli4 });
            NaapuriAttach.SetNaapuriOminaisuus(elli4, new List<Ellipse> { elli3, elli5, elli12 });
            NaapuriAttach.SetNaapuriOminaisuus(elli5, new List<Ellipse> { elli4, elli6 });
            NaapuriAttach.SetNaapuriOminaisuus(elli6, new List<Ellipse> { elli5, elli7, elli14 });
            NaapuriAttach.SetNaapuriOminaisuus(elli7, new List<Ellipse> { elli6, elli8 });
            NaapuriAttach.SetNaapuriOminaisuus(elli8, new List<Ellipse> { elli1, elli7, elli16 });

            NaapuriAttach.SetNaapuriOminaisuus(elli9, new List<Ellipse> { elli16, elli10 });
            NaapuriAttach.SetNaapuriOminaisuus(elli10, new List<Ellipse> { elli2, elli9, elli11, elli18 });
            NaapuriAttach.SetNaapuriOminaisuus(elli11, new List<Ellipse> { elli10, elli12 });
            NaapuriAttach.SetNaapuriOminaisuus(elli12, new List<Ellipse> { elli4, elli11, elli13, elli20 });
            NaapuriAttach.SetNaapuriOminaisuus(elli13, new List<Ellipse> { elli12, elli14 });
            NaapuriAttach.SetNaapuriOminaisuus(elli14, new List<Ellipse> { elli6, elli13, elli15, elli22 });
            NaapuriAttach.SetNaapuriOminaisuus(elli15, new List<Ellipse> { elli16, elli14 });
            NaapuriAttach.SetNaapuriOminaisuus(elli16, new List<Ellipse> { elli8, elli9, elli15, elli24 });

            NaapuriAttach.SetNaapuriOminaisuus(elli17, new List<Ellipse> { elli24, elli18 });
            NaapuriAttach.SetNaapuriOminaisuus(elli18, new List<Ellipse> { elli10, elli17, elli19 });
            NaapuriAttach.SetNaapuriOminaisuus(elli19, new List<Ellipse> { elli18, elli20 });
            NaapuriAttach.SetNaapuriOminaisuus(elli20, new List<Ellipse> { elli12, elli19, elli21 });
            NaapuriAttach.SetNaapuriOminaisuus(elli21, new List<Ellipse> { elli20, elli22 });
            NaapuriAttach.SetNaapuriOminaisuus(elli22, new List<Ellipse> { elli14, elli21, elli23 });
            NaapuriAttach.SetNaapuriOminaisuus(elli23, new List<Ellipse> { elli22, elli24 });
            NaapuriAttach.SetNaapuriOminaisuus(elli24, new List<Ellipse> { elli16, elli17, elli23 });
        }

        private void Radiointi(object sender, RoutedEventArgs e)
        {
            alue.hutiClick();
        }

        public bool plisaysmenossa;


        private List<PeliNappula.Nappula> p1Nappulat = new List<PeliNappula.Nappula>();
        private List<PeliNappula.Nappula> p2Nappulat = new List<PeliNappula.Nappula>();
        private int lisaykset = 0;
        public bool poisto;
        private Random r = new Random();


        public static readonly DependencyProperty pPelaajan1vuoro = DependencyProperty.Register("Pelaajan1vuoro", typeof(bool), typeof(Lauta), new PropertyMetadata(false));

        public Label p1 = new Label(), p2 = new Label();

        public bool pelaajan1Vuoro
        {
            get { return (bool)GetValue(pPelaajan1vuoro); }
            set
            {
                SetValue(pPelaajan1vuoro, (bool)value); if (pelaajan1Vuoro) { p1.FontWeight = FontWeights.Bold; p2.FontWeight = FontWeights.Normal; }
                else { p1.FontWeight = FontWeights.Normal; p2.FontWeight = FontWeights.Bold; }
            }
        }


        private String lisaysapu = "Lisää nappula valitsemalla lisää ja painamalla haluamaasi paikkaa";
        private String poistoapu = "Poista nappula hiiren oikealla näppäimellä";
        private String siirtoapu = "Siirrä nappulaa valitsemalla siirrettävä nappula ja painamalla sen jälkeen haluamaasi paikkaa";
        public TextBlock Helper = new TextBlock();

        private bool peliohi=false;

        private void Klikki(object sender, MouseButtonEventArgs e)
        {
            if (poisto||peliohi) return;
            Ellipse elli;
            foreach (UIElement u in ruudukko.Children)
                if (u is Ellipse) ((Ellipse)u).Fill = Brushes.Black;
            PeliNappula.Nappula nappula;

            if (lisaykset == 18 && (nappula = getCheckedNappula()) != null)
            {
                if (p1Nappulat.Contains(nappula) && !pelaajan1Vuoro) { nappula.Uncheck(); return; }
                if (p2Nappulat.Contains(nappula) && pelaajan1Vuoro) { nappula.Uncheck(); return; }
                List<PeliNappula.Nappula> temp = new List<PeliNappula.Nappula>();
                elli = (Ellipse)sender;
                if ((pelaajan1Vuoro && p1Nappulat.Count > 3) || (!pelaajan1Vuoro && p2Nappulat.Count > 3))
                {
                    List<Ellipse> naapurit = NaapuriAttach.GetNaapuriOminaisuus(nappula);

                    if (!naapurit.Contains(elli))
                    {
                        elli.Fill = Brushes.DarkRed; return;
                    }
                }

                ruudukko.Children.Remove(nappula);
                poistaVaratuista(nappula);
                Grid.SetColumnSpan(nappula, Grid.GetColumnSpan(elli));

                Grid.SetRowSpan(nappula, Grid.GetRowSpan(elli));
                Grid.SetColumn(nappula, Grid.GetColumn(elli));

                Grid.SetRow(nappula, Grid.GetRow(elli));
                if (pelaajan1Vuoro) temp = p1Nappulat;
                else temp = p2Nappulat;
                NaapuriAttach.SetNaapuriOminaisuus(nappula, NaapuriAttach.GetNaapuriOminaisuus(elli));
                ruudukko.Children.Add(nappula);
                varatut.Add(elli);
                if (tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), temp))
                {
                    Helper.Text = poistoapu;
                    poisto = true;
                    return;
                }
                tarkistaVoitto(false);
                pelaajan1Vuoro = !pelaajan1Vuoro;
                return;
            }


            if (plisaysmenossa)
            {
                if (lisaykset == 18) { Helper.Text = siirtoapu; plisaysmenossa = true; return; }
                nappula = new PeliNappula.Nappula();

                if (pelaajan1Vuoro) { nappula.Vari = p1Vari; p1Nappulat.Add(nappula); }
                else { p2Nappulat.Add(nappula); nappula.Vari = p2Vari; }
                lisaykset++;


                elli = (Ellipse)sender;

                Grid.SetColumnSpan(nappula, Grid.GetColumnSpan(elli));

                Grid.SetRowSpan(nappula, Grid.GetRowSpan(elli));
                Grid.SetColumn(nappula, Grid.GetColumn(elli));

                Grid.SetRow(nappula, Grid.GetRow(elli));
                nappula.MouseRightButtonDown += poista;
                NaapuriAttach.SetNaapuriOminaisuus(nappula, NaapuriAttach.GetNaapuriOminaisuus(elli));
                ruudukko.Children.Add(nappula);
                varatut.Add(elli);
                List<PeliNappula.Nappula> temp1;
                if (pelaajan1Vuoro) temp1 = p1Nappulat;
                else temp1 = p2Nappulat;
                if (tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), temp1))
                {
                    Helper.Text = poistoapu;
                    poisto = true; return;
                }
                pelaajan1Vuoro = !pelaajan1Vuoro;

                plisaysmenossa = false;
                if (lisaykset == 18) { Helper.Text = siirtoapu; plisaysmenossa = true; }
            }
        }

        private void poistaVaratuista(PeliNappula.Nappula nap)
        {
            foreach (Ellipse e in varatut)
                if (NaapuriAttach.GetNaapuriOminaisuus(e) == NaapuriAttach.GetNaapuriOminaisuus(nap))
                {
                    varatut.Remove(e);
                    return;
                }
        }


        private void poista(object sender, MouseButtonEventArgs e)
        {
            if (peliohi) return;
            if (poisto)
            {
                PeliNappula.Nappula nappula = (PeliNappula.Nappula)sender;
                if (!pelaajan1Vuoro && p1Nappulat.Contains(nappula))
                {
                    if (!tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), p1Nappulat))
                    {
                        ruudukko.Children.Remove(nappula);
                        poistaVaratuista(nappula);
                        p1Nappulat.Remove(nappula);
                        poisto = false;
                        if (lisaykset < 18) Helper.Text = lisaysapu; else Helper.Text = siirtoapu;
                        tarkistaVoitto(true);
                        pelaajan1Vuoro = !pelaajan1Vuoro;
                    }
                }
                else if (pelaajan1Vuoro && p2Nappulat.Contains(nappula))
                {
                    if (!tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), p2Nappulat))
                    {
                        ruudukko.Children.Remove(nappula);
                        poistaVaratuista(nappula);
                        p2Nappulat.Remove(nappula);
                        poisto = false;
                        if (lisaykset < 18) Helper.Text = lisaysapu; else Helper.Text = siirtoapu;
                        tarkistaVoitto(true);
                        pelaajan1Vuoro = !pelaajan1Vuoro;
                    }
                }

            }

        }
        private List<Ellipse> varatut = new List<Ellipse>();

        private void tarkistaVoitto(bool v)
        {
            if (v)
            {
                if (lisaykset==18&&pelaajan1Vuoro && p2Nappulat.Count < 3) { Helper.Text = "Pelaaja 1 Voittaa";peliohi = true; SystemSounds.Beep.Play();  }
                if (lisaykset==18&&!pelaajan1Vuoro && p1Nappulat.Count < 3) { Helper.Text = "Pelaaja 2 Voittaa"; peliohi = true; SystemSounds.Beep.Play(); }
            }
            else
            {
                bool voitto = true;
                foreach (PeliNappula.Nappula p1Nap in p1Nappulat)
                    foreach (Ellipse e in NaapuriAttach.GetNaapuriOminaisuus(p1Nap))
                        if (!varatut.Contains(e)) { voitto = false; break; }
                if (voitto) { Helper.Text = "Pelaaja 2 Voittaa";SystemSounds.Beep.Play(); peliohi = true; return;  }

                voitto = true;
                foreach (PeliNappula.Nappula p2Nap in p2Nappulat)
                    foreach (Ellipse e in NaapuriAttach.GetNaapuriOminaisuus(p2Nap))
                        if (!varatut.Contains(e)) { voitto = false; break; }

                if (voitto) { Helper.Text = "Pelaaja 1 Voittaa";SystemSounds.Beep.Play(); peliohi = true; return;  }
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



        public PeliNappula.Nappula getCheckedNappula()
        {
            foreach (UIElement nap in ruudukko.Children)
                if (nap is PeliNappula.Nappula)
                    if (((PeliNappula.Nappula)nap).getChecked()) return (PeliNappula.Nappula)nap;
            return null;
        }

        public void hutiClick()
        {
            if (lisaykset < 18) plisaysmenossa = false;
            foreach (UIElement nap in ruudukko.Children)
                if (nap is PeliNappula.Nappula) ((PeliNappula.Nappula)nap).Uncheck();
        }
    }




    public static class NaapuriAttach
    {

        public static readonly DependencyProperty naapuriOminaisuus =
          DependencyProperty.RegisterAttached("naapurit",
          typeof(List<Ellipse>), typeof(NaapuriAttach),
          new UIPropertyMetadata(new List<Ellipse>(), NaapuriOminaisuusChanged));


        public static List<Ellipse> GetNaapuriOminaisuus(DependencyObject obj)
        {
            return (List<Ellipse>)obj.GetValue(naapuriOminaisuus);
        }

        public static void SetNaapuriOminaisuus(DependencyObject obj, List<Ellipse> value)
        {
            obj.SetValue(naapuriOminaisuus, value);
        }

        private static void NaapuriOminaisuusChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }

}
