using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Viikkoteht1
{
    //Ohjelma johon voidaan lisätä tekstikenttiä ja järjestää niitä
    //Toteutettuina toiminnot lisäys ensimmäiseksi ja poisto lopusta
    //järjestäminen kahden kriteerin mukaan nousevasti ja laskevasti
    //exit ja about menu-valinnat toimivat
    //kuten myös undo ja redo
    //muokkaus ei toimi ainakaan vielä
    
    public partial class Form1 : Form
    {
        //lista jolla pidetään yllä järjestystä ja tietoa ohjelman sisältämistä teksti-laatikoista
        private List<Label> tekstiLista=new List<Label>();
        //Undo-pino joka pitää yllä tietoa onko poistettu vai lisätty, true=lisäys, false=poisto 
        //ja mille labelille tämä on tehty
        private Stack<Tuple<Label, bool>> Undo=new Stack<Tuple<Label, bool>>();
        //redo-pino samalla idealla
        private Stack<Tuple<Label, bool>> Redo = new Stack<Tuple<Label, bool>>();

        //pitää yllä tietoa onko sanat nousevassa vai laskevassa järjestyksessä
        private static bool isNouseva=false;

        public Form1()
        {
            InitializeComponent();
        }
        //lisäys-painikkeen toiminto, lisää labeleita halutulla tekstillä
        //ilmoittaa Undo-pinolle muutoksista ja tyhjentää Redo-pinon
        private void button1_Click(object sender, EventArgs e)
        {
            String teksti = lisattavaTeksti.Text.Trim();
            if (!teksti.Equals("  "))
            {
                Label lisattyTeksti = new Label();
                lisattyTeksti.Padding = new Padding(3,3,3,3);
                lisattyTeksti.BackColor = Color.Aqua;
                lisattyTeksti.BorderStyle = BorderStyle.Fixed3D;
                lisattyTeksti.Text = teksti;
                lisattyTeksti.AutoSize = true;
                tekstiLista.Add(lisattyTeksti);
                Undo.Push(new Tuple<Label,bool>(lisattyTeksti,true));
                Redo.Clear();
                
                tekstiAlue.Controls.Add(lisattyTeksti);
            }
        }
        //avaa standardi-muotoisen about-boksin
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 boksi = new AboutBox1();
            boksi.Show();
        }

        //järjestää listan jossa sanat ovat, tyhjentää Tekstialueena toimivan flow-paneelin
        //ja lisää järjestetyt sanat uudestaan
        private void button1_Click_1(object sender, EventArgs e)
        {
            Redo.Clear();Undo.Clear();
            if (isNouseva)
            { 
                tekstiLista=tekstiLista.OrderByDescending(x=>x.Text.Length).ThenByDescending(x=>x.Text).ToList();
                tekstiAlue.Controls.Clear();
                foreach (Label kentta in tekstiLista)
                    tekstiAlue.Controls.Add(kentta);
                isNouseva = false;
                return;
            }
            tekstiLista = tekstiLista.OrderBy(x => x.Text.Length).ThenBy(x => x.Text).ToList();
            tekstiAlue.Controls.Clear();
            foreach (Label kentta in tekstiLista)
                tekstiAlue.Controls.Add(kentta);
            isNouseva = true;
        }

        //poistaa viimeisen alkion listasta, jossa pidetään ohjelman sisältämät sanat
        //tyhjentää näkyvillä olevat sanat ja lisää listan näkyville
        //ilmoittaa Undo-pinolle muutoksista ja tyhjentää Redo-pinon
        private void tekstiAlue_DoubleClick(object sender, EventArgs e)
        {
            if(tekstiLista.Count>=1)
            {
                Label viimeinen= tekstiLista.Last();
                tekstiLista.Remove(viimeinen);
                tekstiAlue.Controls.Clear();
                foreach(Label kentta in tekstiLista)
                    tekstiAlue.Controls.Add(kentta);
                Undo.Push(new Tuple<Label, bool>(viimeinen, false));
                Redo.Clear();
            }
        }
        //sulkee ohjelman
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //tarkistaa onko Undo-pinossa odottavia operaatioita
        //selvittää ollaanko viimeksi tehty poisto- vai lisäysoperaatio ja kumoaa sen
        //ilmoittaa muutoksesta Redo-pinolle
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Undo.Count > 0)
            {
                Tuple<Label, bool> tark = Undo.Pop();
                Redo.Push(tark);
                if (tark.Item2)
                {
                    tekstiAlue.Controls.Remove(tark.Item1);
                    tekstiLista.Remove(tark.Item1);
                    return;
                }
                tekstiAlue.Controls.Add(tark.Item1);
                tekstiLista.Add(tark.Item1);
            }
        }
        //tarkistaa onko redo-pinossa odottelevia operaatioita
        //päivittää undo-pinon vastaavasti
        //jos undo:lla ollaan poistettu, niin lisätään, jos lisätty niin poistettu
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Redo.Count > 0)
            {
                Tuple<Label, bool> tark = Redo.Pop();
                Undo.Push(tark);
                if (tark.Item2)
                {
                    tekstiAlue.Controls.Add(tark.Item1);
                    tekstiLista.Add(tark.Item1);
                    return;
                }
                tekstiAlue.Controls.Remove(tark.Item1);
                tekstiLista.Remove(tark.Item1);
            }
        }
    }
}
