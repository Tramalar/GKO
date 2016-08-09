using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AikaTextBox
{
    /// <summary>
    /// tekstiboksi joka sallii muodossa hh:mm:ss syötettyjä aika-tietoja
    /// </summary>
    public partial class TimeTextBox: TextBox
    {
       
        private ErrorProvider virhe = new ErrorProvider();

        public TimeTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// tarkistetaan että teksti on halutussa muodossa regexin avulla
        /// yritetään parsia ja tarkistetaan onko aika alle 24 tuntia
        /// asetetaan virhe vastaavasti sekä boksi punaiseksi mikäli syöte ei ole halutussa muodossa
        /// </summary>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (!Regex.IsMatch(Text, "[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}"))
            {
                e.Cancel = true;
                this.BackColor = Color.Red;
                virhe.SetError(this, "Aika ei kelvollisessa muodossa, syötä hh:mm:ss");
                base.OnValidating(e);
                return;
            }
            try
            {
                if (TimeSpan.Parse(Text).CompareTo(new TimeSpan(24, 0, 0)) >= 0)
                {
                    e.Cancel = true;
                    this.BackColor = Color.Red;
                    virhe.SetError(this, "Liian pitkä aikaväli! Syötä alle 24h.");
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                this.BackColor = Color.Red;
                virhe.SetError(this, "Aika ei kelvollisessa muodossa, syötä hh:mm:ss");
            }
            base.OnValidating(e);
        }

        /// <summary>
        /// palautetaan boksi valkoiseksi ja asetetaan virhe tyhjäksi
        /// </summary>
        protected override void OnValidated(EventArgs e)
        {

            this.BackColor = Color.White;
            virhe.SetError(this, null);
            base.OnValidated(e);
        }
    }
}
