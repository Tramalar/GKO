using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NumeroTextBox
{
    public partial class NumberTextBox: TextBox
    {
        [Category("NumberTextBox"),
        Description("minimiarvo"),
        DefaultValue(0),
        Browsable(true)]
        public int min
        {
            get { return p_min; }
            set { p_min = value; }
        }
        /// <summary>
        /// minimiarvo, joka boksiin sallitaan
        /// </summary>
        private int p_min;

        [Category("NumberTextBox"),
        Description("maksimiarvo"),
        DefaultValue(1000),
        Browsable(true)]
        public int max
        {
            get { return p_max; }
            set { p_max = value; }
        }
        /// <summary>
        /// maksimiarvo joka boksiin sallitaan
        /// </summary>
        private int p_max=1000;



        public NumberTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// virheentarjoajaluokka
        /// </summary>
        private ErrorProvider virhe = new ErrorProvider();


        /// <summary>
        /// tarkistetaan ensin regexillä että syöte on muotoa luku,desimaalit tai luku.desimaalit
        /// yritetään sitten parsia luku korvaten piste pilkulla ja toisinpäin
        /// tarkistetaan myös onko annettu luku sallitulla alueella [min,max] ja asetetaan virhe vastaavaksi ja boksi punaiseksi mikäli jää kiinni
        /// </summary>
        protected override void OnValidating(CancelEventArgs e)
        {
            double temp;

            if(!Regex.IsMatch(Text,("[0-9]*[,.]?[0-9]*?")))
            {
                e.Cancel = true;
                BackColor = Color.Red;
                virhe.SetError(this, "et syöttänyt numeroa!");
            }
            if (double.TryParse(Text, out temp))
            {
                if (temp < p_min || temp > p_max)
                {
                    e.Cancel = true;
                    BackColor = Color.Red;
                    virhe.SetError(this, String.Format("Syötit virheellisen numeron! Vain luvut väliltä {0} - {1} kelpaavat kelpaavat!", p_min, p_max));
                }
            }
            else if (double.TryParse(Text.Replace('.', ','), out temp)|| double.TryParse(Text.Replace(',', '.'), out temp))
            {
                if (temp < p_min || temp > p_max)
                {
                    e.Cancel = true;
                    BackColor = Color.Red;
                    virhe.SetError(this, String.Format("Syötit virheellisen numeron! Vain luvut väliltä {0} - {1} kelpaavat kelpaavat!", p_min, p_max));
                }
            }
            else
            {
                e.Cancel = true;
                BackColor = Color.Red;
                virhe.SetError(this, "Et syöttänyt numeroa!");
            }
            base.OnValidating(e);
        }
        /// <summary>
        /// laitetaan virhe tyhjäksi ja palautetaan boksi valkoiseksi
        /// </summary>
        protected override void OnValidated(EventArgs e)
        {
            virhe.SetError(this, null);
            BackColor = Color.White;
            base.OnValidated(e);
        }
    }
}
