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

namespace MyllyPeli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Insert.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            InitializeComponent();
        }

        public static RoutedCommand Insert = new RoutedCommand();

        private void ExecutedInsert(object sender, ExecutedRoutedEventArgs e) {
            alue.plisaysmenossa=true ;
        }

        private void CanExecuteInsert(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = !(alue.plisaysmenossa||alue.poisto); }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            alue.hutiClick();
        }
    }
}
