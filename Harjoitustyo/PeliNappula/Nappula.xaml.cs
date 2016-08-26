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

namespace PeliNappula
{
    /// <summary>
    /// Pelissä käytettävät nappulat
    /// </summary>
    public partial class Nappula : UserControl
    {
        public Nappula()
        {
            InitializeComponent();
        }
        /// <summary>
        /// nappulan väriominaisuus
        /// </summary>
        public Brush Vari
        {
            get { return (Brush)GetValue(VariProperty); }
            set { SetValue(VariProperty, value); }
        }

        /// <summary>
        /// asetetaan nappulan sisältämä togglebutton pois valinnasta
        /// </summary>
        public void Uncheck()
        {
            nappula.IsChecked = false;
        }

        /// <summary>
        /// kertoo onko nappulan sisältämä togglebutton valittuna
        /// </summary>
        /// <returns>tieto siitä onko nappula valittuna</returns>
        public bool getChecked()
        {
            return (bool)nappula.IsChecked;
        }

       public static readonly DependencyProperty VariProperty =
            DependencyProperty.Register("Vari", typeof(Brush), typeof(Nappula), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// tarkistus onko väri oletusarvossaan
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeVari()
        {
            return Vari != Brushes.White;
        }
        /// <summary>
        /// palauttaa värin oletukseksi
        /// </summary>
        public void ResetVari()
        {
            Vari = Brushes.White;
        } 
        /// <summary>
        /// routedevent joka nostetaan kun nappulaa painetaan
        /// </summary>
        public static readonly RoutedEvent nappialas = EventManager.RegisterRoutedEvent("nappi alas", RoutingStrategy.Bubble,  typeof(RoutedEventHandler),    typeof(Nappula));
        /// <summary>
        /// nostetaan nappialas tapahtuma nappulan previewmousedown tapahtumassa, tämän avulla tarkistetaan että vain yksi on alas painettuna 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nappula_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RoutedEventArgs neweventargs = new RoutedEventArgs(nappialas);
            nappula.RaiseEvent(neweventargs);
        }
    }
}
