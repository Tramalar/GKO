using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TahtiLabel
{
    /// <summary>
    /// Komponentti joka sisältää tahdin laskemiseen tarvittavia tietoja
    /// </summary>
    public partial class PaceLabel: Label
    {
        /// <summary>
        /// kulunut aika
        /// </summary>
        private TimeSpan pTime;
        /// <summary>
        /// kuljettu matka
        /// </summary>
        private double pDistance;

        /// <summary>
        /// määrittää raja-arvot tapahtumille fast, slow ja average
        /// </summary>
        private int pFastMin=4;
        private int pSlowMin=6;

        [Category("Misc"),
         Description("Minimi-nopeus tapahtumalle fast"),
         DefaultValue(4),
         Browsable(true)]
        public int fastMin {
            get { return pFastMin; }
            set { pFastMin = value; }
        }

        [Category("Misc"),
         Description("Minimi-nopeus tapahtumalle slow"),
         DefaultValue(6),
         Browsable(true)]
        public int slowMin
        {
            get { return pSlowMin; }
            set { pSlowMin = value; }
        }

        [Category("Misc"),
        Description("Treeniin kulunut aika"),
        Browsable(true)]
        public TimeSpan time
        {
            get { return pTime; }
            set { pTime =value; }
        }
        public bool ShouldSerializeTime() {
            return pTime.Equals(new TimeSpan(0, 0, 0));
        }

        public void ResetTime() {
            time = new TimeSpan(0, 0, 0);
        }

        [Category("Misc"),
        Description("Treenin aikana kuljettu matka"),
        Browsable(true),
        DefaultValue(0)]
        public double distance
        {
            get { return pDistance; }
            set { pDistance = value; }
        }



        /// <summary>
        /// delegaatit komponentin tapahtumille
        /// </summary>
        public delegate void GoSlow(object sender, EventArgs e);
        public delegate void GoAverage(object sender, EventArgs e);
        public delegate void GoFast(object sender, EventArgs e);

        /// <summary>
        /// komponentin tapahtumat Fast, Average ja Slow
        /// </summary>
        public event GoFast Fast;
        public event GoAverage Average;
        public event GoSlow Slow;






        public PaceLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// lisätään kuuntelija tapahtumille Fast, Slow ja Average asetettujen raja-arvojen mukaan
        /// </summary>
        private void PaceLabel_TextChanged(object sender, EventArgs e)
        {
            double vauhti = this.time.TotalMinutes/distance;
            if(Average!=null&&vauhti<pSlowMin&&vauhti>=pFastMin)Average(this, e);
            if (Fast != null && vauhti < pFastMin) Fast(this, e);
            if (Slow != null && vauhti >= pSlowMin) Slow(this, e);
        }
    }
}
