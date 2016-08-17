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

namespace AikaBoksi
{

    public class TimeRule : ValidationRule
    {
        /// <summary>
        /// validoidaan tarkistamalla että syöte on muotoa hh:mm:ss ja parsimalla siitä timespan-olio
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            TimeSpan alku = new TimeSpan(0, 0, 0);

            if (!Regex.IsMatch((String)value, "^[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}$"))
            {
                return new ValidationResult(false, "ei aika muodossa hh:mm:ss");
            }
            try
            {
                alku = TimeSpan.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "ei aika muodossa hh:mm:ss");
            }
            if (alku.CompareTo(new TimeSpan(24, 0, 0)) > 0)
            {
                return new ValidationResult(false, "syötä alle 24h");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TimeTextBox : UserControl
    {
        public TimeTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// onko validoituna
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            return !Validation.GetHasError(aika);
        }

        /// <summary>
        /// tieto boksin sisältämästä ajasta
        /// </summary>
        public static readonly DependencyProperty AikaProperty = DependencyProperty.Register("Aika", typeof(TimeSpan), typeof(TimeTextBox), new FrameworkPropertyMetadata(new TimeSpan(0, 0, 0)));
        public TimeSpan Aika
        {
            get { return (TimeSpan)GetValue(AikaProperty); }
            set { SetValue(AikaProperty, value); }
        }

        /// <summary>
        /// palauttaa tekstiboksin sisältämän aikamuuttujan
        /// </summary>
        /// <returns></returns>
        public TimeSpan getAika()
        {
            return TimeSpan.Parse(aika.Text);
        }

    }
}
