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

namespace t3
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// alustetaan ajastin sekä lisätään käsittelijä raahaustapahtumalle
        /// asetetaan pudottaminen mahdolliseksi pääikkunan paneeleissa
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.AddHandler(DragBehaviour.Raahaus, new RoutedEventHandler(raahauksenkasittelija));
            dt = new System.Windows.Threading.DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Tick += lisaaAikaa;
            DragBehaviour.SetIsDroppable(tulokset, true);
            DragBehaviour.SetIsDroppable(kangas, true);
            this.Loaded += MainWindow_Loaded;
            newGame();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            arvoPaikat();
        }

        /// <summary>
        /// lisätään raahausten lukumäärää kun raahaus tapahtuu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raahauksenkasittelija(object sender, RoutedEventArgs e)
        {
            laskuri++;
        }


        /// <summary>
        /// laskee raahausten määrän
        /// </summary>
        public static readonly DependencyProperty laskuriProperty =
    DependencyProperty.Register("laskuri", typeof(int), typeof(MainWindow));
        public int laskuri
        {
            get { return (int)GetValue(laskuriProperty); }
            set { SetValue(laskuriProperty, value); }
        }

        private Label laskurilabel = new Label();
        private int kulunutaika;
        private System.Windows.Threading.DispatcherTimer dt;
        private Label timelabel = new Label();
        private int maara;
        private List<Label> kentat = new List<Label>();

        private void newCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// uuden pelin luominen kutsuu newGame-funktiota joka alustaa pelin, lisäksi tyhjennetään mahdolliset aikaisempien pelien aiheuttamat muutokset ikkunaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            kangas.Children.Clear();
            kentat.Clear();
            tulokset.Background = Brushes.Aqua;
            tulokset.Children.Clear();
            kulunutaika = 0;

            newGame();
            arvoPaikat();
        }




        /// <summary>
        /// alustetaan muuttujat jotka tarvitsee alustaa, lisätään kentälle laskutoimitus arpomalla lukuja ja toimituksia 
        /// </summary>
        public void newGame()
        {
            timelabel.Content = kulunutaika;
            laskuri = 0;
            laskurilabel.DataContext = this;
            laskurilabel.SetBinding(ContentProperty, "laskuri");
            DockPanel.SetDock(laskurilabel, Dock.Right);
            tulokset.Children.Add(laskurilabel);


            DockPanel.SetDock(timelabel, Dock.Right);
            tulokset.Children.Add(timelabel);
            //piilossa kunnes peli on ohi
            timelabel.Visibility = Visibility.Hidden;
            dt.Start();
            Random r = new Random();
            //satunnainen määrä lukuja
            maara = r.Next(2, 4);
            int[] luvut = new int[maara + 1];
            char[] kaytetytToimitukset = new char[maara - 1];
            Label temp;

            //lisätään luvut taulukkoon ja listaan jossa on kaikki arvottavat labelit
            for (int i = 0; i < maara; i++)
            {
                luvut[i] = r.Next(-10, 10);
                temp = new Label();
                temp.Content = luvut[i];
                kentat.Add(temp);
            }

            char[] ToimitusLotto = new char[] { '+', '-' };
            int tulos = luvut[0];

            //arvotaan toimitukset ja lasketaan näin saatava tulos
            for (int i = 1; i < maara; i++)
            {
                kaytetytToimitukset[i - 1] = ToimitusLotto[r.Next(0, 2)];

                temp = new Label();
                temp.Content = kaytetytToimitukset[i - 1];
                kentat.Add(temp);

                switch (kaytetytToimitukset[i - 1])
                {
                    case ('+'): tulos += luvut[i]; break;
                    case ('-'): tulos -= luvut[i]; break;
                }
            }
            luvut[maara] = tulos;
            kaytetytToimitukset[maara - 2] = '=';

            //lisätään vielä tulos ja =-merkki
            temp = new Label();
            temp.Content = tulos;
            kentat.Add(temp);


            temp = new Label();
            temp.Content = '=';
            kentat.Add(temp);


        }

        //arvotaan labelien paikat canvaksella ja asetetan raahattaviksi
        private void arvoPaikat()
        {
            Random r = new Random();
            double x, y;
            if (kangas.ActualHeight < 30) y = 0;
            foreach (Label label in kentat)
            {
                if (kangas.ActualWidth < 30) x = 0;
                else x = r.NextDouble() * (kangas.ActualWidth - 30);
                if (kangas.ActualHeight < 30) y = 0;
                else y = r.NextDouble() * (kangas.ActualHeight - 30);

                DragBehaviour.SetIsDraggable(label, true);
                Canvas.SetLeft(label, x);
                Canvas.SetTop(label, y);
                kangas.Children.Add(label);
            }
        }

        /// <summary>
        ///lisätään kulunutta aika ajastimen tick-operaatiossa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lisaaAikaa(object sender, EventArgs e)
        {
            kulunutaika += 1;
            timelabel.Content = TimeSpan.FromSeconds(kulunutaika);
        }

        /// <summary>
        /// yritetään laskea tulosta dockpaneelissa olevista labeleista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockPanel_Drop(object sender, DragEventArgs e)
        {
            //=-merkin vasen ja oikea puoli
            int[] laskettavat = new int[2];
            String content;

            //paneelista löytyvät luvut ja toimitukset
            List<int> luvut = new List<int>();
            List<char> toimitukset = new List<char>();
            Label label = e.Data.GetData(typeof(Label)) as Label;

            //tällä pidetään huolta ettei tule kahta lukua tai operaatiota peräkkäin
            bool addedluku = false;
            foreach (Control l in tulokset.Children)
            {
                if (l is Label)
                {
                    if (l != laskurilabel && l != timelabel)
                    {
                        content = ((Label)l).Content.ToString();
                        //kaikista labeleista joiden tiedetään sisältävän lukuja tai toimituksia voidaan yrittää parsia lukua ja epäonnistuessa
                        //lisätä toimitus
                        try { luvut.Add(int.Parse(content)); if (addedluku) return; addedluku = true; }
                        catch (FormatException) { toimitukset.Add(content[0]); if (!addedluku) return; addedluku = false; }
                    }
                }
            }
            //tämä pitää tehdä silmukan ulkopuolella koska viimeisimpänä lisätty label ei vielä tässä vaiheessa ole dockpaneelin lapsi
            content = label.Content.ToString();

            try { luvut.Add(int.Parse(content)); if (addedluku) return; addedluku = true; }
            catch (FormatException) { toimitukset.Add(content[0]); if (!addedluku) return; addedluku = false; }

            //jos alle kolme lukua ei voi olla toimiva laskutoimitus joten poistutaan
            if (luvut.Count < 3) return;
            laskettavat[0] = luvut[0];
            int yhtaSuuruudenPuoli = 0;

            //lasketaan löytyvien laskutoimitusten mukaan ja vaihdetaan laskupaikkaa kun =-merkki tulee vastaan
            for (int i = 0; i < luvut.Count - 1; i++)
            {

                switch (toimitukset[i])
                {
                    case ('+'): laskettavat[yhtaSuuruudenPuoli] += luvut[i + 1]; break;
                    case ('-'): laskettavat[yhtaSuuruudenPuoli] -= luvut[i + 1]; break;
                    case ('='): yhtaSuuruudenPuoli = 1; laskettavat[1] = luvut[i + 1]; break;
                }
            }

            //mikäli kaikki labelit käytetty voidaan tarkistaa.
            if ((2 * maara - 1) != (tulokset.Children.Count - 3)) return;
            //jos lasku menee läpi niin laitetaan ajastin näkyville, vaihdetaan taustaväri vihreäksi ja estetään labeleiden raahaaminen
            if (laskettavat[0] == laskettavat[1])
            {
                tulokset.Background = Brushes.Green; dt.Stop();
                timelabel.Visibility = Visibility.Visible;
                foreach (Control c in tulokset.Children)
                {
                    if (c is Label)
                        DragBehaviour.SetIsDraggable((Label)c, false);

                }
                DragBehaviour.SetIsDraggable(label, false);
            }

        }


    }

    public static class DragBehaviour
    {

        /// <summary>
        /// raahaus attachedpropertyna
        /// </summary>
        public static readonly DependencyProperty IsDraggableProperty =
          DependencyProperty.RegisterAttached("IsDraggable",
          typeof(bool), typeof(DragBehaviour),
          new UIPropertyMetadata(false, IsDraggableChanged));

        /// <summary>
        /// droppaus attachedpropertyna
        /// </summary>
        public static readonly DependencyProperty IsDroppableProperty =
          DependencyProperty.RegisterAttached("IsDroppable",
          typeof(bool), typeof(DragBehaviour),
          new UIPropertyMetadata(false, IsDroppableChanged));

        public static bool GetIsDraggable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDraggableProperty);
        }

        public static void SetIsDraggable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDraggableProperty, value);
        }

        public static bool GetIsDroppable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDroppableProperty);
        }

        public static void SetIsDroppable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDroppableProperty, value);
        }

        /// <summary>
        /// lisätään käsittelijä droppiin jos tämä attached property laitetaan toimintaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void IsDroppableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Panel)) return;

            Panel panel = (Panel)sender;
            bool isDroppable = (bool)(e.NewValue);

            if (isDroppable)
            {
                panel.AllowDrop = true;
                panel.Drop += DropLabel;
            }
            if (!isDroppable)
            {
                panel.Drop -= DropLabel;
            }
        }

        /// <summary>
        /// lisätään käsittelijä hiiren liikkeeseen jos asetetaan dragproperty voimaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void IsDraggableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (!(sender is Label)) return;


            Label label = (Label)sender;
            bool isDraggable = (bool)(e.NewValue);

            if (isDraggable)
            {
                label.MouseMove += DragLabel;
            }
            if (!isDraggable)
            {
                label.MouseMove -= DragLabel;
            }
        }


        /// <summary>
        /// poistetaan raahauskohde lähteestään ja lisätään kohteeseen
        /// mikäli kohde on canvas asetetaan se pudotus-kohtaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DropLabel(object sender, DragEventArgs e)
        {
            Label label = (Label)(e.Data.GetData(typeof(Label)));
            Panel panel = (Panel)sender;
            if (panel is Canvas)
            {
                ((Panel)label.Parent).Children.Remove(label);
                Canvas.SetLeft(label, e.GetPosition(panel).X);
                Canvas.SetTop(label, e.GetPosition(panel).Y);
                ((Canvas)panel).Children.Add(label);
                return;
            }

            ((Panel)label.Parent).Children.Remove(label);

            panel.Children.Add(label);
        }

        /// <summary>
        /// raahataan kun hiiri liikkuu lähettäjän päällä ja vasen painike on painettuna, nostetaan raahaustapahtuma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DragLabel(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) return;
            Label label = (Label)sender;
            Panel panel = (Panel)label.Parent;
            DragDrop.DoDragDrop(panel, label, DragDropEffects.Move);
            RoutedEventArgs neweventargs = new RoutedEventArgs(Raahaus);
            label.RaiseEvent(neweventargs);
        }

        /// <summary>
        /// routed event joka nostetaan aina raahattaessa
        /// </summary>
        public static readonly RoutedEvent Raahaus = EventManager.RegisterRoutedEvent("Raahaus", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Label));
    }
}

