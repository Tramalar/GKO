using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyllyPeli
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            tiedot.Text = "Lisää nappuloita ruudulle valitsemalla lisää, joko file-menusta tai painamalla näppäin yhdistelmää ctrl+D, Tämän jälkeen paina haluamaasi ympyrää pelikentällä ja nappulasi ilmestyy siihen.";
        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            tiedot.Text = "Siirtäminen tapahtuu painamalla nappia, jota haluaa siirtää ja tämän jälkeen painamalla haluamaansa ympyrää pelilaudalla. Nappuloita voi siirtää kun kaikki 18 nappulaa on lisätty laudalle, nappuloita siirretään omalla pelivuorolla aina yhden ruudun päähän. Tilanteessa, jossa nappuloita on pelaajalla jäljellä enää 3, saa nappuloita siirtää muuallekin kuin yhden ruudun päähän.";
        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            tiedot.Text = "Jos pelaaja saa muodostettua myllyn eli saa asetettua kolme nappulaa vierekkäin saa hän poistaa vastustajalta yhden nappulan, joka ei ole myllyssä.";
        }

        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            tiedot.Text = "Jos vastapelaajallasi on jäljellä enää 2 nappulaa jäljellä, voitat pelin. Pelin voi voittaa myös saattamalla vastapelaajan tilanteeseen, jossa tällä ei ole enää mahdollisia siirtoja.";
        }

        private void ListBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            tiedot.Text = "Pelin alussa kumpikin pelaaja saa yhdeksän nappia, jotka sijoitetaan laudalle vuorotellen. Kun nappulat on vapaavalintaisesti sijoitettu laudalle, pelaajat koettavat vuorotellen siirtää nappuloita viivojen leikkauspisteistä toisiin viivoja pitkin. Siirron on päätyttävä johonkin lähimpään viivan leikkauspisteeseen, eli nappulaa ei saa kuljettaa ympäri lautaa yhdellä siirrolla. Jos pelaaja onnistuu siirtämään nappulan siten, että samalla viivalla on kolme saman pelaajan nappulaa, eli mylly, hän saa poistaa yhden sellaisen vastustajan nappulan, joka ei ole myllyssä. Paras tilanne on niin sanottu sahamylly, jossa pelaaja pystyy siirtämään yhtä nappulaa edestakaisin siten, että jokaisella siirrolla syntyy mylly.\n Kun pelaajalla on jäljellä kolme nappulaa, voi nappula hyppiä siirrolla mihin tahansa laudalla vapaana olevaan ruutuun.Tämä antaa tasoitusta alakynnessä olevalle pelaajalle, mutta harvoin muuttaa pelin lopputulosta. Pelaaja voittaa mikäli hän onnistuu poistamaan 7(vastustajalle jää vain 2 nappulaa jäljelle, eikä vastustaja voi enää muodostaa myllyä) vastustajan nappulaa laudalta. Myös jumittamalla vastustajan nappulat voi voittaa.";
        }

        private void ListBoxItem_Selected_5(object sender, RoutedEventArgs e)
        {
            tiedot.Text = "Mylly on tilanne pelaajalla on laudalla 3 nappulaa vierekkäin, vuorolla jolla mylly saadaan saa pelaaja poistaa vastustajaltaan yhden nappulan. Lisäksi myllyssä valmiiksi olevia nappuloita ei voi poistaa.";
        }
    }
}
