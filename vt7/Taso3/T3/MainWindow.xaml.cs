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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace T3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// tarkistetaan onko tekstiboksissa tekstiä ja sallitaan tulostaminen sen mukaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            String text = new TextRange(TekstiAlue.Document.ContentStart, TekstiAlue.Document.ContentEnd).Text;
            if (text.Trim().Equals("")) e.CanExecute = false;
            else e.CanExecute = true;
        }

        /// <summary>
        /// tulostamisen toteutus, avataan tulostusdialogi, kopioidaan dokumentti jottei ikkunan tiedot muutu tulostusasetusten mukaan,
        /// asetetaan kopiolle tulostukseen liittyviä asetuksia ja tulostetaan niiden mukaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            dialog.ShowDialog();

            //kopiointi...
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            TextRange source = new TextRange(TekstiAlue.Document.ContentStart, TekstiAlue.Document.ContentEnd);
            source.Save(s, DataFormats.Xaml);
            FlowDocument doc = new FlowDocument();
            TextRange dest = new TextRange(doc.ContentStart, doc.ContentEnd);
            dest.Load(s, DataFormats.Xaml);

            //lisätään ensimmäiseksi blokiksi tulostettavassa tekstissä uusi kappale
            Paragraph otsikko = new Paragraph();
            otsikko.Inlines.Add("Graafisten käyttöliittymien ohjelmointi");
            otsikko.FontSize = 50;
            otsikko.TextAlignment = TextAlignment.Left;
            doc.Blocks.InsertBefore(doc.Blocks.First(), otsikko);

            //asetetaan leipätekstin asetuksia
            doc.LineHeight = 20;
            //sivulle mahtuu näin 25 riviä sekä paddingit
            doc.PageHeight = doc.LineHeight * 25 + 120;
            doc.PageWidth = dialog.PrintableAreaWidth - 20;
            doc.FontSize = 14;
            doc.PagePadding = new Thickness(60, 60, 60, 60);
            doc.ColumnWidth = doc.PageWidth;


            IDocumentPaginatorSource dps = doc;

            //sivuttaja jonka avulla jokaisen sivun alkuun saadaan asetettua haluttu teksti
            OmaSivuttaja sivutus = new OmaSivuttaja(dps.DocumentPaginator);

            dialog.PrintDocument(sivutus, "uusi dokumentti");
        }
    }

    public class OmaSivuttaja : DocumentPaginator
    {

        DocumentPaginator sivuttaja;

        public OmaSivuttaja(DocumentPaginator paginator)
        {
            sivuttaja = paginator;
        }

        /// <summary>
        /// tämän avulla saadaan jokaisen sivun alkuun asetettua haluttu teksti
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public override DocumentPage GetPage(int pageNumber)
        {

            DrawingVisual ylakulma = new DrawingVisual();
            ContainerVisual newpage = new ContainerVisual();

            //tehdään uusi visuaalinen komponentti joka halutaan sivulle lisätä
            using (DrawingContext ctx = ylakulma.RenderOpen())
            {
                FormattedText text = new FormattedText("GKO-Tiea212 ",
                    System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 18, Brushes.Black);
                ctx.DrawText(text, new Point(20, 20));
            }
            //lisätään sivulle sivun alkuperäinen sisältö
            newpage.Children.Add(sivuttaja.GetPage(pageNumber).Visual);
            //sekä yläkulmaan tuleva teksti
            newpage.Children.Add(ylakulma);
            //palautetaan näin muodostunut uusi sivu
            return new DocumentPage(newpage);
        }

        //nämä tarvitaan kun halutaan periä documentpaginatorista...
        public override bool IsPageCountValid
        {
            get
            {
                return sivuttaja.IsPageCountValid;
            }
        }

        public override int PageCount
        {
            get
            {
                return sivuttaja.PageCount;
            }
        }

        public override Size PageSize
        {
            get
            {
                return sivuttaja.PageSize;
            }

            set
            {
                sivuttaja.PageSize = value;
            }

        }
        public override IDocumentPaginatorSource Source
        {
            get
            {
                return sivuttaja.Source;
            }
        }
    }

}
