using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HorizontalBar
{
    /// <summary>
    /// palkit toteutettu Rasterbaria mukaillen
    /// </summary>
    public partial class Bar: UserControl
    {

        private Brush pensseli1;
        private Brush pensseli2;

        /// <summary>
        /// asetetaan propertyt liukuväreille
        /// </summary>
        private Color alku=Color.Black;
        private Color loppu=Color.White;
        [Category("Vari"),
         Browsable(true),
         Description("väri josta liuutaan")]
        public Color alkuvari
        {
            get { return alku; }
            set { alku = value; Bar_SizeChanged(this, null); }
        }
        [Category("Vari"),
         Browsable(true),
         Description("väri jota kohti liuutaan")]
        public Color loppuvari
        {
            get { return loppu; }
            set { loppu = value; Bar_SizeChanged(this, null); }
        }

        public bool ShouldSerializeAlkuvari() {
            return alku == Color.Black;
        }

        public void ResetAlkuvari(){
            alku = Color.Black;
        }

        public bool ShouldSerializeLoppuvari()
        {
            return loppu == Color.White;
        }

        public void ResetLoppuvari()
        {
            loppu = Color.White;
        }


        /// <summary>
        /// alustetaan pensselit luomaan liukuvärit, alkuväristä loppuväriin ja toisinpäin
        /// </summary>
        public Bar()
        {
            pensseli1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, this.ClientSize.Height), alku, loppu);
            pensseli2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, this.ClientSize.Height), loppu, alku);
            InitializeComponent();
        }

        /// <summary>
        /// täytetään palkit luoduilla liukuväripensseleillä, siten että puoleenväliin väritetään pensselillä 1, ja loput pensselillä kaksi
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.FillRectangle(pensseli1, 0, 0, this.ClientSize.Width, this.ClientSize.Height / 2);
            canvas.FillRectangle(pensseli2, 0, this.ClientSize.Height / 2, this.ClientSize.Width, this.ClientSize.Height / 2);
        }

        /// <summary>
        /// leveys muuttumaan vanhemman leveyden muuttuessa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_Resize(object sender, EventArgs e)
        {
            if (AutoSize)
            {
                this.Width = Parent.ClientSize.Width;
            }
        }

        /// <summary>
        /// koon muuttuessa päivitetään pensselit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bar_SizeChanged(object sender, EventArgs e)
        {
            pensseli1.Dispose();
            pensseli2.Dispose();
            pensseli1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, this.ClientSize.Height), alku, loppu);
            pensseli2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, this.ClientSize.Height), loppu, alku);
            Invalidate();

        }


        /// <summary>
        /// alustuksessa asetetaan kääsittelijä vanhemman koon muuttumiselle ja otetaan vanhemman leveys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bar_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Resize += new EventHandler(Parent_Resize);
                if (AutoSize) this.Width = Parent.ClientSize.Width;

            }
        }
    }
}
