using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Treenilaskuri
{


    /// <summary>
    /// ohjelma joka laskee annetuista aika- ja matka-arvoista vauhdin ja ilmoittaa sen minuutteina kilometriä kohti 
    /// </summary>
    public partial class Treenilaskuri : Form
    {
        public Treenilaskuri()
        {
            InitializeComponent();
        }


        /// <summary>
        /// lasketaan pacelabelille matka- ja aikaominaisuudet näille varatuista textboxeista 
        /// koska klikkaaminen ei ole mahdollista jollei tekstiboksien sisältö ole validoitu voidaan parsen onnistumiseen luottaa
        /// koska sallitaan sekä piste että pilkku erottimena pitää tässä vielä yrittää parsia kaikilla mahdollisilla permutaatioilla
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            double temp;
            paceLabel1.time = TimeSpan.Parse(timeTextBox1.Text);
            if(!double.TryParse(numberTextBox1.Text,out temp)&&!double.TryParse(numberTextBox1.Text.Replace('.',','), out temp))
                temp= double.Parse(numberTextBox1.Text.Replace(',','.' ));
            paceLabel1.distance = temp;
            paceLabel1.Text =paceLabel1.time.Hours+" h, "+paceLabel1.time.Minutes+" min, "+paceLabel1.time.Seconds+" s, "+paceLabel1.distance+" km: "+ 
                (paceLabel1.time.TotalMinutes / temp).ToString("0.00")+" min/km";
        }

        /// <summary>
        /// sallitaan lomakkeen sulkeminen vaikka kentissä olisi validoimatonta tekstiä
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }


 
        /// <summary>
        /// validoidaan lomakkeella olevat syöttökentät aina kun jomman kumman teksti muuttuu
        /// jos jommalla kummalla lomakkeella on validoimatonta tietoa voidaan asettaa laskennan suorittava nappi pois käytöstä
        /// </summary>
        private void timeTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (ValidateChildren()) { button1.Enabled = true; return; }
            button1.Enabled = false;
        }

        private void numberTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (ValidateChildren()) { button1.Enabled = true; return; }
            button1.Enabled = false;
        }

        
        /// <summary>
        /// Vaihdetaan lomakkeen väriä ja tekstiä Pacelabelin tapahtumien mukaan
        /// </summary>
        private void paceLabel1_Average(object sender, EventArgs e)
        {
            BackColor = Color.Yellow;
            Text = Name + ": Average";
        }

        private void paceLabel1_Fast(object sender, EventArgs e)
        {
            BackColor = Color.Red;
            Text = Name + ": Fast";
        }

        private void paceLabel1_Slow(object sender, EventArgs e)
        {
            BackColor = Color.Green;
            Text = Name + ": Slow";
        }
    }
}
