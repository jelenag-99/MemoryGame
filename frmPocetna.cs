using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Memorija
{
    public partial class frmPocetna : Form
    {
        /*Forma koja se prikazuje prilikom pokretanja aplikacije i sadrži
          dugmad za izbor broja polja sa kojima se radi
         */
        public frmPocetna()
        {
            InitializeComponent();
        }

        private void btn44_Click(object sender, EventArgs e) //događaj koji se poziva kada korisnik klikne na dugme 4x4 na formi
        {
            this.Hide(); //skrivanje prikaza trenutne forme 
            frmMemorija memorija = new frmMemorija(4); //kreiranje nove instance glavne forme sa proslijeđenom vrijednošću 4 što predstavlja broj polja po redu i po koloni
            memorija.ShowDialog(); //prikazivanje prethodno kreirane forme
        }

        private void btn88_Click(object sender, EventArgs e) //događaj koji se poziva kada korisnik klikne na dugme 8x8 na formi
        {
            this.Hide(); //skrivanje prikaza trenutne forme 
            frmMemorija memorija = new frmMemorija(8); //kreiranje nove instance glavne forme sa proslijeđenom vrijednošću 8 što predstavlja broj polja po redu i po koloni
            memorija.ShowDialog(); //prikazivanje prethodno kreirane forme
        }
    }
}
