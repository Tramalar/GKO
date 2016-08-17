using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rullailija
{
    public partial class Scroller: UserControl
    {
        Graphics canvas;
        private Font pFontti;
        /// <summary>
        /// property fontin säätämiseksi
        /// </summary>
        public Font fontti
        {
            get { return pFontti; }
            set { pFontti = value; }
        }
        public bool ShouldSerializeFontti() {
            return pFontti.Equals(new Font("Arial",12));
        }
        public void ResetFontti()
        {
            pFontti = new Font("Arial", 12);
        }


        private int pNopeus=5;
        /// <summary>
        /// property nopeuden säätämiseksi
        /// </summary>
        public int nopeus
        {
            get { return pNopeus; }
            set { pNopeus = value; }
        }

        /// <summary>
        /// property liikkuvan tekstin säätämiseksi
        /// </summary>
        private string pTeksti="Teksti";
        public string teksti
        {
            get { return pTeksti; }
            set { pTeksti = value; }
        }

        /// <summary>
        /// sijainnin laskemiseen tarvittavat luvut
        /// </summary>
        private int xkoord=0;
        private int ykoord=0;

        public Scroller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// piirretään kuva xkoord-ja ykoord-muuttujien osoittamaan paikkaan
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            canvas = e.Graphics;
           canvas.DrawString(pTeksti,pFontti,Brushes.White,new Point(xkoord,ykoord));
        }

        /// <summary>
        /// jos vanhemman koko muuttu, tätäkin pitää kasvattaa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_Resize(object sender, EventArgs e)
        {
            if (AutoSize)
            {
                this.Width = Parent.ClientSize.Width+50;
            }
        }

        /// <summary>
        /// asetetaan alustuksessa tämän leveydeksi vanhempansa leveys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scroller_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Resize += new EventHandler(Parent_Resize);
                if (AutoSize) this.Width = Parent.ClientSize.Width+50;

            }
        }

        /// <summary>
        /// siirretään alkuun mikäli ollaan ylitetty leveys 
        /// muutoin mennään kaksi vaihetta sinikäyrää skaalattuna ikkunan leveyteen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (xkoord >= Width) xkoord = -50;
            xkoord +=pNopeus;
            ykoord =(int)(Height/2.5-Math.Cos(4*Math.PI*xkoord/Width)*Height/2.5);
            Invalidate();
        }

  
    }
}
