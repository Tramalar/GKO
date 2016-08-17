using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Komposiitti
{

    /// <summary>
    /// komento laskunapin painamiselle
    /// </summary>
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Laske = new RoutedUICommand
                ("laske", "laske",  typeof(CustomCommands));   
    }


/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class Treenilaskuri : UserControl
    {
        public Treenilaskuri()
        {
            InitializeComponent();
            tahti.fast += new WpfControlLibrary2.UserControl1.GoFast(tahti_fast);
            tahti.average += new WpfControlLibrary2.UserControl1.GoAverage(tahti_average);
            tahti.slow += new WpfControlLibrary2.UserControl1.GoSlow(tahti_slow);
        }

        /// <summary>
        /// käsittelijät pacelabelin tapahtumille
        /// </summary>
        private void tahti_slow()
        {
            Background=Brushes.Green;
        }

        private void tahti_average()
        {
            Background=Brushes.Yellow;
        }

        private void tahti_fast()
        {
            Background = Brushes.Red;
        }

        /// <summary>
        /// laskenta on mahdollista vain jos molemmat boksit ovat valideja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaskeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(distancebox.isValid()&&timebox.isValid())
                e.CanExecute = true;
        }

        /// <summary>
        /// lasketaan tahti kun laske-komento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaskeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Aika = timebox.getAika();
                Matka = distancebox.getNumber();
                tahti.aika = Aika;
                tahti.matka = Matka;
            }
            catch (FormatException)
            {
                Aika = new TimeSpan(0, 0, 0);
                Matka = 0;
            }
            tahti.setText((Aika.TotalMinutes / Matka).ToString("0.00") + " min/km");
        }
        private TimeSpan Aika;
        private Double Matka;


    }
}
