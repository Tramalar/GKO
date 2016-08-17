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

namespace WpfControlLibrary2
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// kuljettu matka
        /// </summary>
        private double pMatka=0;
        public double matka
        { get { return pMatka; }
            set { pMatka=value; } }

        /// <summary>
        /// käytetty aika
        /// </summary>
        private TimeSpan pAika = new TimeSpan(0, 0, 0);
            public TimeSpan aika {
            get { return pAika; }
            set { pAika = value; }
        }
        
            
        private int pSlowMin=6;
        
        /// <summary>
        /// raja-arvot tapahtumille slow, fast ja average
        /// </summary>
        public int SlowMin
        {
            get { return (pSlowMin); }
            set { pSlowMin = value; }
        }
        private int pFastMax=4;
        public int fastMax
        {
            get { return (pFastMax); }
            set { pFastMax= value; }
        }

        /// <summary>
        /// delegaatit komponentin tapahtumille
        /// </summary>
        public delegate void GoSlow();
        public delegate void GoAverage();
        public delegate void GoFast();

        /// <summary>
        /// komponentin tapahtumat Fast, Average ja Slow
        /// </summary>
        public event GoFast fast;
        public event GoAverage average;
        public event GoSlow slow;

        /// <summary>
        /// kutsutaan tapahtumia kun ehdot täyttyy
        /// </summary>
        /// <param name="value"></param>
        public void setText(String value)
        {
            tahti.Content = value;
            double vauhti = aika.TotalMinutes / matka;
            if (vauhti > pSlowMin && slow != null) { slow(); return; }
            if (vauhti < pFastMax && fast != null) { fast(); return; }
            if (average != null) { average();return;}
        }
     
    }
}
