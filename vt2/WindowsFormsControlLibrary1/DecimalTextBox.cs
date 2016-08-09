using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesimaaliTextBox
{
    public partial class DecimalTextBox: TextBox
    {
        private int p_min;

        private int p_max;

        [Category("DecimalTextBox"),
        Description("minimiarvo"),
        DefaultValue(0),
        Browsable(true)]
        public int min {
            get { return p_min; }
            set { p_min=value; }
        }

        [Category("DecimalTextBox"),
        Description("maksimiarvo"),
        DefaultValue(1000),
        Browsable(true)]
        public int max {
            get { return p_max; }
            set { p_max = value; }
        }


        public DecimalTextBox()
        {
            InitializeComponent();
        }
        
        private ErrorProvider virhe = new ErrorProvider();

        protected override void OnValidating(CancelEventArgs e)
        {
            double temp;
            if(Text.Contains(',') && double.TryParse(Text, out temp))
            { 
                if (temp < p_min || temp>p_max )
                {
                    e.Cancel = true;
                    virhe.SetError(this, String.Format("Syötit virheellisen numeron! Vain luvut väliltä {0} - {1} kelpaavat kelpaavat!",p_min,p_max));
                }
            }
            else if (!Text.Contains('.')&&double.TryParse(Text.Replace(',', '.'), out temp))
            {
                if (temp < p_min || temp > p_max)
                {
                    e.Cancel = true;
                    virhe.SetError(this, String.Format("Syötit virheellisen numeron! Vain luvut väliltä {0} - {1} kelpaavat kelpaavat!", p_min, p_max));
                }
            }
            else
            {
                e.Cancel = true;
                virhe.SetError(this, "Et syöttänyt numeroa!");
            }
            base.OnValidating(e);
        }
        protected override void OnValidated(EventArgs e)
        {
            virhe.SetError(this, null);
            base.OnValidated(e);
        }
    }
}
