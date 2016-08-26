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

    /// <summary>
    /// Ellipsien koon muuttamiseen tarkoitettu muunnin. Tehdään ellipseistä kaksi kolmasosaa yhden ruudun koosta 
    /// </summary>
    public class Skaalaus : IValueConverter
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

    /// <summary>
    /// konvertteri keskellä olevien viivojen piirtämistä auttamaan
    /// </summary>
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
    /// Pelilauta ja pelin logiikka
    /// </summary>
    public partial class Lauta : UserControl
    {
        /// <summary>
        /// pelaajan nappuloiden 1 väri
        /// </summary>
        public Brush p1Vari = Brushes.Bisque;
        /// <summary>
        /// pelaajan 2 nappuloiden väri
        /// </summary>
        public Brush p2Vari = Brushes.Red;

        /// <summary>
        /// luodaan lauta, kuunnellaan, nappuloiden routed eventiä joka kertoo kun nappula on valittuna
        /// arvotaan aloittava pelaaja, asetetaan avustusteksti neuvomaan lisäämisestä
        /// asetetaan peliruuduille naapurit
        /// </summary>
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

        /// <summary>
        /// vaihdetaan pelaajan nappuloiden väriä ja värjätään kaikki nappulat jotka kuuluu väriä vaihtaneelle pelaajalle
        /// </summary>
        /// <param name="onkoPelaaja1">Kertoo kumman pelaajan väriä ollaan vaihtamassa true=pelaaja1 ja false=pelaaja2</param>
        /// <param name="vari">Väri joka asetetaan pelaajan nappuloiden väriksi</param>
        public void varjaa(bool onkoPelaaja1, Brush vari)
        {
            if (onkoPelaaja1) { p1Vari = vari; foreach (PeliNappula.Nappula n in p1Nappulat) n.Vari = vari; p1.Background = vari; }
            else { p2Vari = vari; foreach (PeliNappula.Nappula n in p2Nappulat) n.Vari = vari; p2.Background = vari; }
        }

        /// <summary>
        /// asetetaan jokaiselle ruudulla olevalle peliruudulle tämän vieressä sijaitsevat ruudut naapureiksi
        /// </summary>
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

        /// <summary>
        /// Tämän avulla vain yksi nappula voi olla kerrallaan valittuna pelilaudalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radiointi(object sender, RoutedEventArgs e)
        {
            alue.hutiClick();
        }

        /// <summary>
        /// kertoo ollaanko lisäystilassa, eli voidaanko tehdä uusia lisäyksiä
        /// </summary>
        public bool plisaysmenossa;

        /// <summary>
        /// pelaajan 1 nappulat
        /// </summary>
        private List<PeliNappula.Nappula> p1Nappulat = new List<PeliNappula.Nappula>();
        /// <summary>
        /// pelaajan 2 nappulat
        /// </summary>
        private List<PeliNappula.Nappula> p2Nappulat = new List<PeliNappula.Nappula>();
        /// <summary>
        /// pitää yllä laudalle lisättyjen nappuloiden määrää, tämän saavuttaessa arvon 18 siirrytään lisäysvaiheesta siirtelyvaiheeseen
        /// </summary>
        private int lisaykset = 0;
        /// <summary>
        /// Kertoo onko poisto menossa, jos on ei laudalla tehdä muuta kuin odotetaan poistoa
        /// </summary>
        public bool poisto;
        /// <summary>
        /// tällä arvotaan aloittava pelaaja
        /// </summary>
        private Random r = new Random();

        /// <summary>
        /// dependancy property tiedolle siitä kumman pelaajan vuoro on menossa
        /// </summary>
        public static readonly DependencyProperty pPelaajan1vuoro = DependencyProperty.Register("Pelaajan1vuoro", typeof(bool), typeof(Lauta), new PropertyMetadata(false));

        /// <summary>
        /// Labelit jotka kertovat kumman pelaajan vuoro on
        /// </summary>
        public Label p1 = new Label(), p2 = new Label();

        /// <summary>
        /// pelaajan 1 vuoro propertyna
        /// </summary>
        public bool pelaajan1Vuoro
        {            
            get { return (bool)GetValue(pPelaajan1vuoro); }
            //asetetaan arvo pelaajan vuorolle, boldataan vuorossa olevan pelaajan labeli
            set
            {
                SetValue(pPelaajan1vuoro, (bool)value); if (pelaajan1Vuoro) { p1.FontWeight = FontWeights.Bold; p2.FontWeight = FontWeights.Normal; }
                else { p1.FontWeight = FontWeights.Normal; p2.FontWeight = FontWeights.Bold; }
            }
        }

        /// <summary>
        /// Aputekstit ja textblock joka näyttää ne
        /// näkyvät pelialueen laidalla kertoen mitä toimintoa vuorossa olevalta pelaajalta odotetaan tällä hetkellä
        /// </summary>
        private String lisaysapu = "Lisää nappula valitsemalla lisää ja painamalla haluamaasi paikkaa";
        private String poistoapu = "Poista nappula hiiren oikealla näppäimellä";
        private String siirtoapu = "Siirrä nappulaa valitsemalla siirrettävä nappula ja painamalla sen jälkeen haluamaasi paikkaa";
        public TextBlock Helper = new TextBlock();

        /// <summary>
        /// kertoo onko peli päättynyt, jos on, uusia siirtoja ei enää voi tehdä
        /// </summary>
        private bool peliohi = false;

        /// <summary>
        /// tapahtuu kun ruudulla olevaa ellipsiä eli peliruutua klikataan
        /// jos laudalle on lisätty jo kaikki nappulat, ja jokin nappula on valittuna toteutetaan siirto
        /// jos lisäysvaihe on vielä menossa, ja lisäys on valittuna, toteutetaan lisäys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Klikki(object sender, MouseButtonEventArgs e)
        {
            if (poisto || peliohi) return;
        
            PeliNappula.Nappula nappula;
            Ellipse  elli = (Ellipse)sender;

            if (lisaykset == 18 && (nappula = getCheckedNappula()) != null)
            {
                siirtoLogiikka(nappula, elli);
                return;
            }

            if (plisaysmenossa)
            {
            if (lisaykset == 18) { Helper.Text = siirtoapu; plisaysmenossa = true; return; }
            nappula = new PeliNappula.Nappula();
                lisaysLogiikka(nappula, elli);
            }
        }

        /// <summary>
        /// lisätään laudalle vuorossa olevan pelaajan nappula valittuun peliruutuun
        /// </summary>
        /// <param name="nappula">nappula joka lisätään</param>
        /// <param name="elli">peliruutu johon lisätään</param>
        private void lisaysLogiikka(PeliNappula.Nappula nappula, Ellipse elli)
        {
            if (pelaajan1Vuoro) { nappula.Vari = p1Vari; p1Nappulat.Add(nappula); }
            else { p2Nappulat.Add(nappula); nappula.Vari = p2Vari; }
            lisaykset++;
            
            //asetetaan nappula samaan kohtaan missä klikattu ellipsi sijaitsee
            Grid.SetColumnSpan(nappula, Grid.GetColumnSpan(elli));
            Grid.SetRowSpan(nappula, Grid.GetRowSpan(elli));
            Grid.SetColumn(nappula, Grid.GetColumn(elli));
            Grid.SetRow(nappula, Grid.GetRow(elli));

            //lisätään nappulalle oikean hiiren napin painallukseen poisto-operaatio
            nappula.MouseRightButtonDown += poista;
            //asetetaan nappulan naapureiksi sen alla olevan peliruudun naapurit ja lisätään nappula ruudulle
            NaapuriAttach.SetNaapuriOminaisuus(nappula, NaapuriAttach.GetNaapuriOminaisuus(elli));
            ruudukko.Children.Add(nappula);
            //lisätään ellipsi varattujen listaan
            varatut.Add(elli);
            List<PeliNappula.Nappula> temp1;
            if (pelaajan1Vuoro) temp1 = p1Nappulat;
            else temp1 = p2Nappulat;
            //tarkistetaan tuliko myllyä, jos tuli siirrytään poistotilaan ja palataan
            if (tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), temp1))
            {
                Helper.Text = poistoapu;
                poisto = true; return;
            }
            //vaihdetaan pelaajan vuoroa
            pelaajan1Vuoro = !pelaajan1Vuoro;
            //sallitaan lisääminen mikäli, kaikki nappulat eivät ole vielä laudalla
            plisaysmenossa = false;
            if (lisaykset == 18) { Helper.Text = siirtoapu; plisaysmenossa = true; }
        }

        /// <summary>
        /// Siirtämisen toteutus
        /// </summary>
        /// <param name="nappula"> nappula jota siirretään</param>
        /// <param name="elli"> ellipsi johon yritetään siirtää</param>
        private void siirtoLogiikka(PeliNappula.Nappula nappula, Ellipse elli)
        {
            //värjätään väärien siirtoyritysten takia punaisena olevat ruudut takaisin mustiksi
            foreach (UIElement u in ruudukko.Children)if (u is Ellipse) ((Ellipse)u).Fill = Brushes.Black;
            //poistutaan jos yritetään siirtää vastustajan nappulaa
            if (p1Nappulat.Contains(nappula) && !pelaajan1Vuoro) { nappula.Uncheck(); return; }
            if (p2Nappulat.Contains(nappula) && pelaajan1Vuoro) { nappula.Uncheck(); return; }
            List<PeliNappula.Nappula> temp = new List<PeliNappula.Nappula>();
            //jos nappuloita on enemmän kuin 3 tarkistetaan että siirto tehdään viereisiin ruutuihin.
            if ((pelaajan1Vuoro && p1Nappulat.Count > 3) || (!pelaajan1Vuoro && p2Nappulat.Count > 3))
            {
                List<Ellipse> naapurit = NaapuriAttach.GetNaapuriOminaisuus(nappula);

                if (!naapurit.Contains(elli))
                {
                    elli.Fill = Brushes.DarkRed; return;
                }
            }
            //poistetaan nappula sen nykyiseltä paikalta ja siirretään valittuun uuteen paikkaan
            //samat operaatiot kuin lisäysvaiheessa
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
            //tarkistetaan saatiinko aikaan jumitusvoitto
            tarkistaVoitto(false);
            pelaajan1Vuoro = !pelaajan1Vuoro;
        }

        /// <summary>
        /// poistetaan nappulan alla oleva ellipsi varatuista
        /// koska ellipsillä ja nappulalla on samat naapurit siinä ja vain siinä tapauksessa että ne ovat päällekkäin
        /// poistetaan sellainen ellipsi varatuista jolla on samat naapurit kuin nappulalla jota siirretään
        /// </summary>
        /// <param name="nap"> Nappula joka siirtyy tai poistuu vapauttaen näin allaan olevan ellipsin</param>
        private void poistaVaratuista(PeliNappula.Nappula nap)
        {
            foreach (Ellipse e in varatut)
                if (NaapuriAttach.GetNaapuriOminaisuus(e) == NaapuriAttach.GetNaapuriOminaisuus(nap))
                {
                    varatut.Remove(e);
                    return;
                }
        }

        /// <summary>
        /// Tapahtuu kun pelinappulan päällä painetaan hiiren oikeaa näppäintä
        /// poistetaan klikattu nappula mikäli se on vastustajan
        /// poistutaan jos klikattu nappula on myllyssä
        /// jos kuitenkin kaikki vastapelaajan nappulat on myllyssä voidaan poistaa mikä vaan vastapelaajan nappuloista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        PoistonLogiikka(nappula);
                    }
                    else
                    {
                        foreach (PeliNappula.Nappula n in p1Nappulat)
                        {
                            if (!tarkistaMylly(Grid.GetRow(n), Grid.GetColumn(n), p1Nappulat)) return;

                        }
                        PoistonLogiikka(nappula);
                    }
                }
                else if (pelaajan1Vuoro && p2Nappulat.Contains(nappula))
                {
                    if (!tarkistaMylly(Grid.GetRow(nappula), Grid.GetColumn(nappula), p2Nappulat))
                    {
                        PoistonLogiikka(nappula);
                    }
                    else
                    {
                        foreach (PeliNappula.Nappula n in p2Nappulat)
                        {
                            if (!tarkistaMylly(Grid.GetRow(n), Grid.GetColumn(n), p2Nappulat)) return;

                        }
                        PoistonLogiikka(nappula);
                    }
                }

            }

        }

        /// <summary>
        /// poistetaan valittu nappula ja vaihdetaan vuoroa
        /// tarkistetaan tuliko voitto poistamalla tarpeeksi vastapelaajan nappuloita
        /// </summary>
        /// <param name="nappula"> poistettava nappula</param>
        private void PoistonLogiikka(PeliNappula.Nappula nappula)
        {

            ruudukko.Children.Remove(nappula);
            poistaVaratuista(nappula);
            if (pelaajan1Vuoro) p2Nappulat.Remove(nappula);
            else p1Nappulat.Remove(nappula);
            poisto = false;
            if (lisaykset < 18) Helper.Text = lisaysapu; else Helper.Text = siirtoapu;
            tarkistaVoitto(true);
            pelaajan1Vuoro = !pelaajan1Vuoro;
        }
        /// <summary>
        /// varatut peliruudut
        /// </summary>
        private List<Ellipse> varatut = new List<Ellipse>();

        /// <summary>
        /// tarkistetaan voitto
        /// jos kutsutaan poiston jälkeen tarkistetaan onko vastapelaajalla alle 3 nappulaa ja ilmoitetaan voitosta jos näin on
        /// jos kutsutaan siirron jälkeen tarkistetaan onko vastapelaajalla mahdollisia siirtoja, jos ei, ilmoitetaan voitosta
        /// </summary>
        /// <param name="OnkoNormaaliVoitto"></param>
        private void tarkistaVoitto(bool OnkoNormaaliVoitto)
        {
            if (OnkoNormaaliVoitto)
            {
                if (lisaykset == 18 && pelaajan1Vuoro && p2Nappulat.Count < 3) { Helper.Text = "Pelaaja 1 Voittaa!"; peliohi = true; SystemSounds.Beep.Play(); }
                if (lisaykset == 18 && !pelaajan1Vuoro && p1Nappulat.Count < 3) { Helper.Text = "Pelaaja 2 Voittaa!"; peliohi = true; SystemSounds.Beep.Play(); }
            }
            else
            {
                bool voitto = true;
                //mikäli jokin peliruutu joka sijaitsee jonkin pelaajan nappulan vieressä ei ole varatuissa
                //on pelaajalla jäljellä mahdollisia siirtoja, joten tällöin ei tule voittoa
                //mikäli jokaisen nappulan jokainen naapuri on varatuissa on vastapelaaja voittanut
                foreach (PeliNappula.Nappula p1Nap in p1Nappulat)
                    foreach (Ellipse e in NaapuriAttach.GetNaapuriOminaisuus(p1Nap))
                        if (!varatut.Contains(e)) { voitto = false; break; }
                if (voitto) { Helper.Text = "Pelaaja 2 Voittaa!"; SystemSounds.Beep.Play(); peliohi = true; return; }

                voitto = true;
                foreach (PeliNappula.Nappula p2Nap in p2Nappulat)
                    foreach (Ellipse e in NaapuriAttach.GetNaapuriOminaisuus(p2Nap))
                        if (!varatut.Contains(e)) { voitto = false; break; }

                if (voitto) { Helper.Text = "Pelaaja 1 Voittaa!"; SystemSounds.Beep.Play(); peliohi = true; return; }
            }

        }

        /// <summary>
        /// tarkistetaan mylly
        /// käytetään hyödyksi tietoa että 3 vierekkäistä nappulaa voivat sijaita ainoastaan samalla rivillä, tai samalla sarakkeella gridissä
        /// ja että yhdellä rivillä tai sarakkeella voi olla ainoastaan 3 nappulaa poislukien rivi ja sarake 4
        /// rivillä ja sarakkeella neljä tarkistetaan siis vain puoleen väliin ruudukkoa
        /// </summary>
        /// <param name="rivi">rivi jolla nappula sijaitsee</param>
        /// <param name="sarake">sarake jolla nappula sijaitsee</param>
        /// <param name="nappulat">pelaajan 1 tai pelaajan 2 nappulat</param>
        /// <returns></returns>
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


        /// <summary>
        /// palauttaa valittuna olevan nappulan
        /// </summary>
        /// <returns> valittuna oleva nappula, null jos ketään ei ole valittuna</returns>
        public PeliNappula.Nappula getCheckedNappula()
        {
            foreach (UIElement nap in ruudukko.Children)
                if (nap is PeliNappula.Nappula)
                    if (((PeliNappula.Nappula)nap).getChecked()) return (PeliNappula.Nappula)nap;
            return null;
        }

        /// <summary>
        /// peruutetaan lisäys ja otetaan valinta pois kaikista nappuloista
        /// </summary>
        public void hutiClick()
        {
            if (lisaykset < 18) plisaysmenossa = false;
            foreach (UIElement nap in ruudukko.Children)
                if (nap is PeliNappula.Nappula) ((PeliNappula.Nappula)nap).Uncheck();
        }
    }


    /// <summary>
    /// määritellään täällä attached property jolla voidaan antaa naapureita kontrolleille
    /// </summary>
    public static class NaapuriAttach
    {

    /// <summary>
    /// attached property jolla voidaan liittää lista ellipsejä mille tahansa kontrollille
    /// </summary>
        public static readonly DependencyProperty naapuriOminaisuus =
          DependencyProperty.RegisterAttached("naapurit",
          typeof(List<Ellipse>), typeof(NaapuriAttach),
          new UIPropertyMetadata(new List<Ellipse>(), NaapuriOminaisuusChanged));

        /// <summary>
        /// palauttaa objektille asetetut naapurit
        /// </summary>
        /// <param name="obj">objekti keneltä kysytään naapureista</param>
        /// <returns>objektin naapurit</returns>
        public static List<Ellipse> GetNaapuriOminaisuus(DependencyObject obj)
        {
            return (List<Ellipse>)obj.GetValue(naapuriOminaisuus);
        }

        /// <summary>
        /// asetetaan naapurit
        /// </summary>
        /// <param name="obj">lista ellipseistä joka halutaan objektiin liittää</param>
        /// <param name="value">objekti johon naapurit liitetään</param>
        public static void SetNaapuriOminaisuus(DependencyObject obj, List<Ellipse> value)
        {
            obj.SetValue(naapuriOminaisuus, value);
        }

        private static void NaapuriOminaisuusChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }

}
