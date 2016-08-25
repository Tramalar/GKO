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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Nappula : UserControl
    {
        public Nappula()
        {
            InitializeComponent();
        }
        
        public Brush Vari
        {
            get { return (Brush)GetValue(VariProperty); }
            set { SetValue(VariProperty, value); }
        }


        public void Uncheck()
        {
            nappula.IsChecked = false;
        }


        public bool getChecked()
        {
            return (bool)nappula.IsChecked;
        }


        // Using a DependencyProperty as the backing store for Vari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VariProperty =
            DependencyProperty.Register("Vari", typeof(Brush), typeof(Nappula), new PropertyMetadata(Brushes.White));

        public bool ShouldSerializeVari()
        {
            return Vari != Brushes.White;
        }
        

        private void nappula_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RoutedEventArgs neweventargs = new RoutedEventArgs(nappialas);
            nappula.RaiseEvent(neweventargs);
        }
        public void ResetVari()
        {
            Vari = Brushes.White;
        }
        public static readonly RoutedEvent nappialas = EventManager.RegisterRoutedEvent("nappi alas", RoutingStrategy.Bubble,  typeof(RoutedEventHandler),    typeof(Nappula));
    }
}
