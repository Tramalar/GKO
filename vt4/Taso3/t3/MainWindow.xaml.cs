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

namespace vt4
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
        private void newCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
        /// <summary>
        /// lisätään stackpaneliin uusia laskureita kun kutsutaan New-komentoa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            paneeli.Children.Add(new Komposiitti.Treenilaskuri());
        }

        /// <summary>
        /// suljetaan lomake kun kutsutaan close-komentoa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeExecuted(object sender,ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
