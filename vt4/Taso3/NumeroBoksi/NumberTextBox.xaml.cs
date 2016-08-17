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



namespace NumeroBoksi
{

    public class NumberRule : ValidationRule
    {
        /// <summary>
        /// validoidaan siten että tarkistetaan ensin että syöte on muotoa luku+mahdollinen erotinmerkki+mahdollinen luku
        /// ja sitten yritetään parsia lukua
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            Double alku = 0;
            
            if (!Regex.IsMatch((String)value, "[0-9].?[0-9]?"))
            {
                return new ValidationResult(false, "ei numero");
            }
            try
            {
                alku = Double.Parse((String)value);
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "ei numero");
            }
            return ValidationResult.ValidResult;
        }
    }
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NumberTextBox : UserControl
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// dependency proprty jossa, tieto kuljetusta matkasta
        /// </summary>
        public static readonly DependencyProperty MatkaProperty = DependencyProperty.Register("Matka", typeof(Double), typeof(NumberTextBox), new FrameworkPropertyMetadata(0.0));
        public Double Matka
        {
            get { return (Double)GetValue(MatkaProperty); }
            set { SetValue(MatkaProperty, value); }
        }

        /// <summary>
        /// palauttaa tiedon onko boksi validoitu
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            return !Validation.GetHasError(matkaboksi);
        }

        /// <summary>
        /// palauttaa boksin sisältämän luvun
        /// </summary>
        /// <returns></returns>
        public Double getNumber()
        {
            try
            {
            return Double.Parse(matkaboksi.Text);
            }
            catch (Exception) { return 0; }
        }
    }
}
