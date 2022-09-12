using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;

/*
  PROBAJ NAPRAVITI DODATNU FORMU GDJE CE GA PITATI KOLIKO POLJE ZELI
  I POVEZATI JE S OVOM, A OVU PRILAGODITI DA PRIHVATA TAJ PODATAK
  I NA OSNOVU TOGA PRIKAZUJE PLOCU SA TOLIKO SLIKA, SAMO
  DODAJ TE SLIKE SVE U OVAJ NIZ SLIKA, A ONDA KAD IZABERE VELICINU IZ
  TOG NIZA UZMES KOLIKO TI TREBA I STAVIS U HASH TABELU,
  NESTO NA TAJ FAZON PROBAJ*/
/*NISTA NIJE OBAVEZNO SAMO AKO STIGNES :D */ 
namespace Memorija
{
    public partial class frmMemorija : Form
    {
        List<Image> slike = new List<Image>{Properties.Resources.img1, Properties.Resources.img2, Properties.Resources.img3,
                                                   Properties.Resources.img4, Properties.Resources.img5, Properties.Resources.img6,
                                                   Properties.Resources.img7, Properties.Resources.img8}; //Lista slika koje će biti korišćenje u igri
        Hashtable slikaPcb = new Hashtable(); //Hash tabela za smještanje parova pictureBox i trenutna slika koju on sadrži
        Random lokacija = new Random();
        List<Image> kopijaSlike = new List<Image>(); //Pomoćna lista za smještanje slika prilikom raspoređivanja
        int milisekunde = 0;//Brojac proteklog vremena u sekundama
        Image cover = Properties.Resources.cover;

        Thread tTimer = null;
        Thread t1 = null;
        Thread t2 = null;
        Thread t3 = null;
        Thread t4 = null;

        public frmMemorija()
        {
            InitializeComponent();
        }

        private void frmMemorija_Load(object sender, EventArgs e)
        {
            List<int> brojevi = new List<int>();
            cover.Tag = 0;

            for (int i = 1; i <= slike.Count; i++)  //Dodavanje svih slika i njihovih duplikata u listu kopijaSlika i postavljanje istog taga na sliku i njenu kopiju
            {
                kopijaSlike.Add(slike[i - 1]);
                kopijaSlike.Add(slike[i - 1]);
                kopijaSlike[2 * i - 2].Tag = i;
                kopijaSlike[2 * i - 1].Tag = i;
            }

            for (int i = 0; i < kopijaSlike.Count; i++)  //Kreiranje niza brojeva koji služi za raspoređivanje slika po slučajnom pristupu
            {
                brojevi.Add(i);
            }

            foreach (PictureBox pcb in pnlSlike.Controls)
            {
                int broj = lokacija.Next(0, brojevi.Count - 1); //Slučajan izbor jednog od brojeva koji pripadaju opsegu [0-(velicina liste brojevi-1)]
                int pozicija = brojevi[broj]; //Pronalazak vrijednosti u listi brojevi koja se nalazi na mjestu dohvaćenim pomoću indeksa gore pronađenog
                slikaPcb.Add(pcb, kopijaSlike[pozicija]);  //Dodavanje u hash tabelu para (trenutni pcb, slika iz liste na poziciji nađenoj u prethodnom redu)
                brojevi.Remove(pozicija);  //Brisanje gore izabranog broja iz liste, da bi se izbjeglo pojavljivanje iste slike više od 2 puta

                pcb.Image = cover;  //Postavljanje podrazumijevane slike u sve pictureBox-ove
            }

            tTimer = new Thread(new ThreadStart(delegate {
                tmrProtekloVrijeme_Tick(sender, e);
            }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Label.CheckForIllegalCrossThreadCalls = false;
            tTimer.Start();
            tTimer.Priority = ThreadPriority.Highest;

            /*OVO ZAKOMENTARISANO NEMOJ BRISATI, TO JE KAD IZABERE SA JEDNIM TREDOM DA RADI */

            /*milisekunde = 0;
            tmrProtekloVrijeme.Start();
            for (int i = 0; i < pnlSlike.Controls.Count; i++)
            {
                tmrProtekloVrijeme.Start();
                PictureBox prvaOtvorena = (PictureBox)pnlSlike.Controls[i];
                if (prvaOtvorena.Image.Tag == cover.Tag)
                {
                    prvaOtvorena.Image = (Image)slikaPcb[prvaOtvorena];
                    System.Threading.Thread.Sleep(1000);
                    tmrProtekloVrijeme.Start();
                    prvaOtvorena.Update();
                    lblVrijeme.Update();

                    int j = (i + 1);
                    PictureBox drugaOtvorena = null;
                    do
                    {
                        drugaOtvorena = (PictureBox)pnlSlike.Controls[j];
                        if (drugaOtvorena.Image.Tag == cover.Tag)
                        {
                            drugaOtvorena.Image = (Image)slikaPcb[drugaOtvorena];
                            System.Threading.Thread.Sleep(1000);
                            tmrProtekloVrijeme.Start();
                            drugaOtvorena.Update();

                            if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag)
                            {
                                drugaOtvorena.Image = cover;
                                System.Threading.Thread.Sleep(1000);
                                drugaOtvorena.Update();
                            }
                        }
                        j++;

                    }

                    while (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag && j < pnlSlike.Controls.Count);
                }
            }
            //tmrProtekloVrijeme.Stop();*/

            /*otvaranjeKvadranta(0, 2, 4);
            otvaranjeKvadranta(2, 4, 4);
            otvaranjeKvadranta(8, 10, 4);
            otvaranjeKvadranta(10, 12, 4);*/

            t1 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(0, 2, 4); }));
            t2 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(2, 4, 4); }));
            t3 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(8, 10, 4); }));
            t4 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(10, 12, 4); otvaranjeKvadranta(0, 4, 8); }));

            /* PROBAJ ISTRAZITI KAKO OTKRITI KAD JE ZAVRŠEN POSAO U SVIM TREDOVIMA
              PA TEK ONDA DA SE DODA OVO ŠTO JE U T4 DODATO DA OTVARA SVE*/

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            /* OVO SAM POKUSAVALA OVO GORE STO SAM TI REKLA DA ISTRAZIS AL NE RADI OVAKO*/

            if(t1.ThreadState==ThreadState.Stopped && t2.ThreadState==ThreadState.Stopped && t3.ThreadState==ThreadState.Stopped && t4.ThreadState==ThreadState.Stopped)
            {
                bool postojeNeotvorene = false;

                foreach (PictureBox pcb in pnlSlike.Controls)
                {
                    if (pcb.Image.Tag == cover.Tag)
                    {
                        postojeNeotvorene = true;
                        break;
                    }
                }

                if (postojeNeotvorene == true)
                {
                    t1 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(0, 4, 4); }));
                    t1.Start();
                }
            }
        }

        /*TIMER NE RADI, POSTAVILA SAM GA U POSEBAN TRED AL OPET NECE, IZGLEDA DA OVAJ SLEEP OPET PRAVI
         * PROBLEM, AKO STA IZGUGLAS SLOBODNO PROBAJ*/
        private void tmrProtekloVrijeme_Tick(object sender, EventArgs e)
        {
            milisekunde++;
            lblVrijeme.Text = milisekunde.ToString();
        }

        /*FJA ZA OTVARANJE KVADRANATA, ZA ONAJ OD 4 SLIKE RADI, A ZA VECE NE, NSM IMALA VREMENA DA
         * DETALJNO RAZMISLJAM STA JE PROBLEM AKO STIGNES PROBAJ TI SRACUNATI STA DA SE PROSLJEDJUJE
         * DA RADI ISPRAVNO I ZA KVADRANT OD 4 I OD 16 I ZA CIJELU PLOCU
         * KRECE OD DONJEG DESNOG POLJA TO JE INDEKS 0*/
        void otvaranjeKvadranta(int pocetakReda, int krajReda, int velicinaKvadranta)
        {
            tmrProtekloVrijeme.Start();
            int i = 0;
            int brojac = 0;
            /*BROJAC DA BI MI PRESLO I U GORNJI RED JER IDU INDEKSI NPR 0, 1 PA ONDA 3, 4, NE MOZE DIREKTNO*/
            while (brojac < velicinaKvadranta / 2 - 1)
            {
                for (i = pocetakReda + brojac * velicinaKvadranta; i < krajReda + brojac * velicinaKvadranta; i++)
                {
                    PictureBox prvaOtvorena = (PictureBox)pnlSlike.Controls[i];
                    if (prvaOtvorena.Image.Tag == cover.Tag)
                    {
                        prvaOtvorena.Image = (Image)slikaPcb[prvaOtvorena];
                        Thread.Sleep(1000);
                        prvaOtvorena.Update();

                        int j = (i + 1);
                        PictureBox drugaOtvorena = null;
                        if (j < krajReda + velicinaKvadranta-1)
                        {
                            do
                            {
                                drugaOtvorena = (PictureBox)pnlSlike.Controls[j];
                                if (drugaOtvorena.Image.Tag == cover.Tag)
                                {
                                    drugaOtvorena.Image = (Image)slikaPcb[drugaOtvorena];
                                    Thread.Sleep(1000);
                                    drugaOtvorena.Update();

                                    if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag)
                                    {
                                        drugaOtvorena.Image = cover;
                                        Thread.Sleep(1000);
                                        drugaOtvorena.Update();
                                    }
                                }
                                j++;

                            }

                            while (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag && j < krajReda);
                        }
                        /*DOVDE PRODJE PRVI RED PA ONDA AKO NIJE NADJEN PAR PRELAZI U GORNJI RED*/
                        if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag)
                        {
                            j = pocetakReda + velicinaKvadranta;
                            do
                            {
                                drugaOtvorena = (PictureBox)pnlSlike.Controls[j];
                                if (drugaOtvorena.Image.Tag == cover.Tag)
                                {
                                    drugaOtvorena.Image = (Image)slikaPcb[drugaOtvorena];
                                    Thread.Sleep(1000);
                                    drugaOtvorena.Update();

                                    if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag)
                                    {
                                        drugaOtvorena.Image = cover;
                                        Thread.Sleep(1000);
                                        drugaOtvorena.Update();
                                    }
                                }
                                j++;

                            }

                            while (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag && j < krajReda + velicinaKvadranta);
                        }
                        /*AKO NIJE NADJEN PAR KARTICI ONOJ PRVOJ KOJA JE OTVORENA DA SE I ONA ZATVORI*/
                        if(drugaOtvorena.Image.Tag!=prvaOtvorena.Image.Tag)
                        { 
                            prvaOtvorena.Image = cover;
                            Thread.Sleep(1000);
                            prvaOtvorena.Update();
                        }
                    }
                }
                brojac++;
            }
            /*ZA ZADNJI RED DA NE IDE DO POSLJEDNJE JER AKO BI OTVORILO I POSLJEDNJU U OVO PRVAOTVORENA
             * NE BI BILA NIJEDNA DRUGA DA JE OTVORI DA JOJ TRAZI PARA PA PRAVI PROBLEM*/
            for (i = pocetakReda + brojac * velicinaKvadranta; i < krajReda + brojac * velicinaKvadranta - 1; i++)
            {
                PictureBox prvaOtvorena = (PictureBox)pnlSlike.Controls[i];
                if (prvaOtvorena.Image.Tag == cover.Tag)
                {
                    prvaOtvorena.Image = (Image)slikaPcb[prvaOtvorena];
                    Thread.Sleep(1000);
                    prvaOtvorena.Update();
                    lblVrijeme.Update();

                    int j = (i + 1);
                    PictureBox drugaOtvorena = null;
                    if (j < krajReda + brojac*velicinaKvadranta)
                    {
                        do
                        {
                            drugaOtvorena = (PictureBox)pnlSlike.Controls[j];
                            if (drugaOtvorena.Image.Tag == cover.Tag)
                            {
                                drugaOtvorena.Image = (Image)slikaPcb[drugaOtvorena];
                                Thread.Sleep(1000);
                                drugaOtvorena.Update();

                                if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag)
                                {
                                    drugaOtvorena.Image = cover;
                                    Thread.Sleep(1000);
                                    drugaOtvorena.Update();
                                }
                            }
                            j++;

                        }

                        while (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag && j < krajReda);
                    }


                    if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag)
                    {
                        prvaOtvorena.Image = cover;
                        Thread.Sleep(1000);
                        prvaOtvorena.Update();
                    }
                }
            }
        }
    }
}
