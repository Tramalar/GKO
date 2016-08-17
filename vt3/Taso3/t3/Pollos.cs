using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vt3
{
    public partial class Pollos : Form
    {
        /// <summary>
        /// kuvan nopeus
        /// </summary>
        int nopeus=5;
        int palkkiHeight = 80;

        Image kuva;
        /// <summary>
        /// pitää yllä palkkeja ja niiden nopeuksia
        /// </summary>
        List<Tuple<HorizontalBar.Bar,int>> bars;
        Bitmap uusiKuva;
        Point sijainti = new Point(0, 0);

        Point barSijainti = new Point(0, 0);
        public Pollos()
        {
            bars = new List<Tuple<HorizontalBar.Bar,int>>();
            for (int i = 0; i < 5; i++) {
                HorizontalBar.Bar palkki = new HorizontalBar.Bar();
                palkki.AutoSize = true;
                palkki.Top = i * (palkkiHeight+20);
                palkki.alkuvari = Color.Blue;
                palkki.loppuvari = Color.Orange;
                palkki.Left = 0;
                palkki.Height = palkkiHeight;
                Controls.Add(palkki);
                bars.Add(new Tuple<HorizontalBar.Bar, int>(palkki,20));
            }         
            kuva = Image.FromFile("kani.png");
            InitializeComponent();

            scroller1.BringToFront();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            dc.DrawImageUnscaled(kuva, sijainti);
        }

        /// <summary>
        /// siirretään kuvaa, käännytään aina ohjelman laidan saavuttamisen jälkeen
        /// siirretään palkkeja ja käännetään suunta huipulla ja pohjalla, jolloin myös tuodaan viimeisin kääntynyt palkki etummaiseksi
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (nopeus > 0 && sijainti.X > Width-kuva.Width/2) nopeus = -5;
            if (nopeus < 0 && sijainti.X < 0) nopeus = 5;
            sijainti.X  += nopeus;
            uusiKuva = new Bitmap(kuva.Width, kuva.Height);
            Graphics k = Graphics.FromImage(uusiKuva);
            k.DrawImage(kuva, sijainti);
            uusiKuva.Dispose();
            for(int i=0;i<5;i++)
            {
                barSijainti= new Point(bars[i].Item1.Location.X, bars[i].Item1.Location.Y + bars[i].Item2);
                bars[i].Item1.Location = barSijainti;
                if (bars[i].Item2 > 0 && bars[i].Item1.Bottom > Bottom)
                { bars[i] = new Tuple<HorizontalBar.Bar, int>(bars[i].Item1, -20);
                    if (i > 0)
                    {
                        barSijainti= new Point(bars[i].Item1.Location.X, bars[i - 1].Item1.Location.Y + palkkiHeight+20);
                        bars[i].Item1.Location = barSijainti;
                    }
                }
                if (bars[i].Item2 < 0 && bars[i].Item1.Top < -60)
                { bars[i] = new Tuple<HorizontalBar.Bar, int>(bars[i].Item1, 20); bars[i].Item1.BringToFront(); this.scroller1.BringToFront();
                  if (i > 0)
                    {
                        barSijainti = new Point(bars[i].Item1.Location.X, bars[i - 1].Item1.Location.Y -palkkiHeight-20);
                        bars[i].Item1.Location = barSijainti;
                    }
                }
            }


            Invalidate();
        }
    }
}
