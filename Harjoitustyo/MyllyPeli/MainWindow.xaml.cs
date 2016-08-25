using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            alue.p1.Content = "pelaaja1"; alue.p2.Content = "pelaaja2";

            DockPanel.SetDock(alue.p1, Dock.Top);
            telakka.Children.Add(alue.p1);
            DockPanel.SetDock(alue.p2, Dock.Top);
            telakka.Children.Add(alue.p2);
            DockPanel.SetDock(alue.Helper, Dock.Top);
            telakka.Children.Add(alue.Helper);
        }

        public static RoutedCommand About = new RoutedCommand();
        public static RoutedCommand Insert = new RoutedCommand();

        private void CanExecuteAbout(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExecutedAbout(object sender, ExecutedRoutedEventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog();
        }


        private void ExecutedInsert(object sender, ExecutedRoutedEventArgs e) {
            alue.plisaysmenossa=true ;
        }

        private void CanExecuteInsert(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = !(alue.plisaysmenossa||alue.poisto); }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            alue.hutiClick();
        }

        private void Settings_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Dialogi asetukset = new Dialogi();
            asetukset.ShowDialog();
            if (asetukset.DialogResult == true)
            {
                if (!asetukset.pelaaja1.Trim().Equals("")) alue.p1.Content = asetukset.pelaaja1;
                if (!asetukset.pelaaja2.Trim().Equals("")) alue.p2.Content = asetukset.pelaaja2;
                if (!(asetukset.Vari1 == null)) alue.varjaa(true,asetukset.Vari1);
                if (!(asetukset.Vari2 == null)) alue.varjaa(false,asetukset.Vari2);
            }
            
            if (asetukset.DialogResult == false) return;
        }

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }        
                 
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ruudukko.Children.Remove(alue);
            String temp1 = (String)alue.p1.Content, temp2 = (String)alue.p2.Content;
            Brush tempvari1 = alue.p1Vari,tempvari2=alue.p2Vari;
            telakka.Children.Remove(alue.p1); telakka.Children.Remove(alue.p2); telakka.Children.Remove(alue.Helper);
            alue= new Pelialue.Lauta();
            alue.p2.Content = temp2;alue.p1.Content = temp1;
            Grid.SetColumn(alue, 1);
            DockPanel.SetDock(alue.p1, Dock.Top);
            telakka.Children.Add(alue.p1);
            DockPanel.SetDock(alue.p2, Dock.Top);
            telakka.Children.Add(alue.p2);
            alue.p2Vari = tempvari2;alue.p1Vari = tempvari1;

            telakka.Children.Add(alue.Helper);
            ruudukko.Children.Add(alue);
        }
        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
    }
}
