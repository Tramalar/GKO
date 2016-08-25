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
    /// Interaction logic for Dialogi.xaml
    /// </summary>
    public partial class Dialogi : Window
    {
        public Dialogi()
        {
            InitializeComponent();
        }

        public Brush Vari1
        {
            get { if (valittuvari1.SelectedColor == null) return null; return new SolidColorBrush((Color)valittuvari1.SelectedColor); }
        }

        public Brush Vari2
        {
            get { if (valittuvari2.SelectedColor == null) return null; return new SolidColorBrush((Color)valittuvari2.SelectedColor); }
        }

        public string pelaaja1
        {
            get { return p1Nimi.Text; }
        }

        public string pelaaja2
        {
            get
            {
                return p2Nimi.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
