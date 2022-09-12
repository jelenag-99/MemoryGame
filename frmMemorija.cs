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

namespace Memorija
{
    public partial class frmMemorija : Form
    {
        List<Image> slike = new List<Image>{
        Properties.Resources.img1, Properties.Resources.img2, Properties.Resources.img3,
        Properties.Resources.img4, Properties.Resources.img5, Properties.Resources.img6,
        Properties.Resources.img7, Properties.Resources.img8, Properties.Resources.img9,
        Properties.Resources.img10, Properties.Resources.img11, Properties.Resources.img12,
        Properties.Resources.img13,Properties.Resources.img14, Properties.Resources.img15,
        Properties.Resources.img16, Properties.Resources.img17,Properties.Resources.img18,
        Properties.Resources.img19, Properties.Resources.img20, Properties.Resources.img21,
        Properties.Resources.img22, Properties.Resources.img23, Properties.Resources.img24,
        Properties.Resources.img25, Properties.Resources.img26, Properties.Resources.img27,
        Properties.Resources.img28,Properties.Resources.img29, Properties.Resources.img30,
        Properties.Resources.img31,Properties.Resources.img32, Properties.Resources.img33,
        Properties.Resources.img34, Properties.Resources.img35,Properties.Resources.img36,
        Properties.Resources.img37, Properties.Resources.img38,Properties.Resources.img39,
        Properties.Resources.img40, Properties.Resources.img41, Properties.Resources.img42,
        Properties.Resources.img43, Properties.Resources.img44,Properties.Resources.img45,
        Properties.Resources.img46, Properties.Resources.img47, Properties.Resources.img48,
        Properties.Resources.img49,Properties.Resources.img50, Properties.Resources.img51,
        Properties.Resources.img52,Properties.Resources.img53, Properties.Resources.img54,
        Properties.Resources.img55,Properties.Resources.img56,Properties.Resources.img57,
        Properties.Resources.img58, Properties.Resources.img59, Properties.Resources.img60,
        Properties.Resources.img61,Properties.Resources.img62,Properties.Resources.img63,
        Properties.Resources.img64
        }; //kreiranje liste svih slika koje se mogu koristiti u aplikaciji zbog lakše naknadne manipulacije
        Hashtable slikaPcb = new Hashtable(); //Hash tabela za smještanje parova pictureBox i trenutna slika koju on sadrži
        Random lokacija = new Random(); //Nova instanca klase Random za slučajno raspoređivanje slika po PictureBox-ovima
        List<Image> kopijaSlike = new List<Image>(); //Pomoćna lista za smještanje svih slika i njihovih duplikata koje će se koristiti na ploči
        static int sekunde = 0;//Brojač proteklog vremena u izvršavanju simulacije u sekundama
        Image cover; //Slika koja se prikazuje prije pokretanja simulacije u svim poljima

        Thread t1 = null; //Kreiranje nove niti za izvršavanje simulacije u okviru prvog kvadranta
        Thread t2 = null; //Kreiranje nove niti za izvršavanje simulacije u okviru drugog kvadranta
        Thread t3 = null; //Kreiranje nove niti za izvršavanje simulacije u okviru trećeg kvadranta
        Thread t4 = null; //Kreiranje nove niti za izvršavanje simulacije u okviru četvrtog kvadranta

        int velicina = 0; //Varijabla koja sadrži veličinu ploče proslijeđenu sa početne forme
        private static System.Windows.Forms.Timer timer; //Kreiranje instance tajmera koji služi za mjerenje proteklog vremena
        public frmMemorija(int velicina) //Konstruktor forme koji prima parametar veličina koji ukazuje na broj polja po redu i po veličini na ploči
        {
            InitializeComponent();
            this.velicina = velicina; //Smještanje proslijeđene vrijednosti sa početne forme u lokalnu varijablu veličina
            kreirajPlocu(); //Kreiranje ploče sa poljima u skladu sa proslijeđenom vrijednošću, veličine 4x4 ili 8x8
        }

        private void Timer_Tick(object sender, EventArgs e)  //Funkcija koja se poziva na svaki otkucaj tajmera
        {
            Label.CheckForIllegalCrossThreadCalls = false; //Omogućavanje pristupanja labeli koja prikazuje proteklo vrijeme iz različitih niti
            sekunde++; //Uvećavanje broja sekundi za jednu
            lblVrijeme.Text = sekunde.ToString(); //Ispis trenutne vrijednosti sekundi u labeli na formi
        }

        private void kreirajPlocu() //Funkcija za kreiranje ploče sa poljima
        {
            pnlSlike.Height = 90*this.Height/100; //Definisanje visine panela koji sadrži PictureBox-ove u zavisnosti od visine cijele forme
            pnlSlike.Width = this.Width * 70 / 100; //Definisanje visine panela koji sadrži PictureBox-ove u zavisnosti od širine cijele forme
            Point lokacija = new Point(8, 3); //Definisanje lokacije tačke koja predstavlja gornji lijevi ugao panela
            for (int i = 0; i < velicina * velicina; i++) //For petlja za kreiranje potrebnog broja PictureBox-ova, tj. polja za prikaz slika
            {
                string ime = "pcb" + (i + 1); //Kreiranje naziva svakog PictureBox-a u zavisnosti od rednog broja, npr. pcb1
                PictureBox pcb = new PictureBox(); //Kreiranje nove instance klase PictureBox
                pcb.Name = ime; //Dodjela vrijednosti prethodno kreiranog stringa ime imenu novokreiranog PictureBox-a
                pcb.Height = pnlSlike.Height / velicina-5; //Postavljanje visine novog PictureBox-a u zavisnosti od visine panela i broja polja po redu sadržanog u vrijednosti varijable veličina
                pcb.Width = pnlSlike.Width / velicina-10; //Postavljanje širine novog PictureBox-a u zavisnosti od visine panela i broja polja po koloni sadržanog u vrijednosti varijable veličina
                pcb.SizeMode = PictureBoxSizeMode.StretchImage; //Omogućavanje ispravnog prikaza slike čak i kada su dimenzije slike veće od dimenzija PictureBox-a
                pcb.BorderStyle = BorderStyle.FixedSingle; //Postavljanje bordera oko polja za prikaz slike
                pcb.Location = lokacija; //Postavljanje lokacije gornjeg lijevog ugla novog PictureBox-a na vrijednost lokalne varijable lokacija
                lokacija.X += pcb.Width + 5; //Povećanje vrijednosti koordinate X tačke lokacija za širinu novog PictureBox-a uvećanu za 5
                if ((i + 1) % velicina == 0) //Ako se dođe do kraja jednog reda panela prelazak u novi red
                {
                    lokacija.Y += pcb.Height + 5; //Povećanje vrijednosti koordinate Y tačke lokacija za visinu novog PictureBox-a uvećanu za 5
                    lokacija.X = 8; //Postavljanje koordinate X tačke lokacija na vrijednost 8
                }
                pcb.Visible = true; //Omogućavanje prikaza novog PictureBox-a na formi
                pnlSlike.Controls.Add(pcb); //Dodavanje novog PictureBox-a u niz kontrola koje su sadržane u panelu

            }
        }

        private void frmMemorija_Load(object sender, EventArgs e) //Funkcija koja se poziva prilikom učitavanja forme
        {
            cover = Properties.Resources.pozadina4; //Postavljanje slike koja će se prikazivati u svim poljima prije pokretanja simulacije i simbolizovati zatvoreno polje
            List<int> brojevi = new List<int>(); //Kreiranje niza brojeva koji služi za slučajno raspoređivanje slika po poljima
            cover.Tag = 0;

            for (int i = 1; i <= (velicina * velicina / 2); i++)  //Dodavanje svih slika i njihovih duplikata u listu kopijaSlika i postavljanje istog taga na sliku i njenu kopiju
            {
                kopijaSlike.Add(slike[i - 1]);
                kopijaSlike.Add(slike[i - 1]);
                kopijaSlike[2 * i - 2].Tag = i;
                kopijaSlike[2 * i - 1].Tag = i;
            }

            for (int i = 0; i < kopijaSlike.Count; i++)  //Popunjavanje niza brojeva koji služi za raspoređivanje slika po slučajnom pristupu vrijednošću svih indeksa iz niza slika i njihovih kopija
            {
                brojevi.Add(i);
            }

            foreach (PictureBox pcb in pnlSlike.Controls) //Prolazak kroz sve kontrole tipa PictureBox smještene u panelu pnlSlike
            {
                int broj = lokacija.Next(0, brojevi.Count - 1); //Slučajan izbor jednog od brojeva koji pripadaju opsegu [0-(velicina liste brojevi-1)]
                int pozicija = brojevi[broj]; //Pronalazak vrijednosti u listi brojevi koja se nalazi na mjestu dohvaćenom pomoću iznad pronađenog indeksa 
                slikaPcb.Add(pcb, kopijaSlike[pozicija]);  //Dodavanje u hash tabelu para (trenutni pcb, slika iz liste na poziciji nađenoj u prethodnom redu)
                brojevi.Remove(pozicija);  //Brisanje gore izabranog broja iz liste, da bi se izbjeglo pojavljivanje iste slike više od 2 puta

                pcb.Image = cover;  //Postavljanje podrazumijevane slike u sve pictureBox-ove
            }
        }

        private void btnStart_Click(object sender, EventArgs e) //Funkcija koja se poziva prilikom klika na dugme koje označava izvršavanja simulacije uz korišćenje paralelizacije
        {
            Label.CheckForIllegalCrossThreadCalls = false; //Omogućavanje pristupanja labelama na formi iz različitih niti

            timer = new System.Windows.Forms.Timer();//Kreiranje nove instance tajmera koji mjeri vrijeme izvršavanja simulacije koja se pokreće
            timer.Interval = 1000; //Postavljanje intervala tajmera na 1000 ms, tj. da tajmer otkucava svake sekunde
            timer.Enabled = true; //Omogućavanje rada tajmera
            timer.Tick += Timer_Tick; //Postavljanje funkcije Timer_Tick kao funkcije koja će se pozivati prilikom svakog otkucaja tajmera
            timer.Start(); //Pokretanje tajmera

            t1 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(0, velicina / 2); })); //Postavljanje poziva funkcije za izvršavanje simulacije u prvom kvadrantu u novu nit koja se dodjeljuje varijabli t1
            t2 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(velicina / 2, velicina / 2); })); //Postavljanje poziva funkcije za izvršavanje simulacije u drugom kvadrantu u novu nit koja se dodjeljuje varijabli t2
            t3 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(velicina * velicina / 2, velicina / 2); })); //Postavljanje poziva funkcije za izvršavanje simulacije u trećem kvadrantu u novu nit koja se dodjeljuje varijabli t3
            t4 = new Thread(new ThreadStart(delegate { otvaranjeKvadranta(velicina * velicina / 2 + velicina / 2, velicina / 2); otvaranjeCijelogPolja(); timer.Stop(); })); //Postavljanje poziva funkcije za izvršavanje simulacije u četvrtom kvadrantu, te funkcije za sekvencijalno otvaranje neotvorenih polja i stopiranje tajmera u novu nit koja se dodjeljuje varijabli t4

            t1.Start(); //Pokretanje izvršavanja niti t1
            t2.Start(); //Pokretanje izvršavanja niti t2
            t3.Start(); //Pokretanje izvršavanja niti t3
            t4.Start(); //Pokretanje izvršavanja niti t4

        }

        private void otvaranjeKvadranta(int pocetnoPolje, int velicinaKvadranta) //Funkcija za otvaranje polja određenog kvadranta, u zavisnosti od proslijeđenog početnog polja i veličine kvadranta
        {
            PictureBox.CheckForIllegalCrossThreadCalls = false; //Omogućavanje pristupa PictureBox-ovima na formi iz različitih niti
            for (int i = pocetnoPolje; i < pocetnoPolje + velicinaKvadranta * velicina - 1; i++) //Prolazak kroz sva polja zadatog kvadranta
            {
                PictureBox prvaOtvorena = (PictureBox)pnlSlike.Controls[i]; //Smještanje trenutnog polja u varijablu prvaOtvorena
                if (prvaOtvorena.Image.Tag == cover.Tag) //Provjeravanje da li je polje smješteno u prvaOtvorena zatvoreno, ako je zatvoreno izvršavanje narednog koda, ako nije ignorisanje polja i prelazak na naredno
                {
                    prvaOtvorena.Image = (Image)slikaPcb[prvaOtvorena]; //Postavljanje slike koja odgovara polju u Hash mapi kao slike trenutnog pictureBox-a umjesto cover slike
                    System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u otvaranju polja
                    prvaOtvorena.Update(); //Prikaz izmijenjene verzije polja

                    int j = (i + 1); //Uzimanje indeksa koji je sljedići u nizu iza indeksa trenutnog polja
                    PictureBox drugaOtvorena = null; //Kreiranje varijable drugaOtvorena tipa PictureBox za smještanje polja prilikom traženja para polju iz varijable prvaOtvorena
                    do //Prolazak kroz polja kvadranta dok se ne nađe odgovarajući par ili dok se ne prođe kroz sva polja
                    {
                        if (j % velicinaKvadranta == 0) //Ako se dođe do kraja reda u sklopu kvadranta prelazak na polje koje se nalazi na početku sljedećeg reda istog kvadranta 
                            j += velicinaKvadranta;

                        if (j < pocetnoPolje + velicina * velicinaKvadranta) //Provjera da li se došlo do posljednjeg polja u kvadrantu
                        {
                            drugaOtvorena = (PictureBox)pnlSlike.Controls[j]; //Smještanje polja sa indeksom j u varijablu drugaOtvorena
                            if (drugaOtvorena.Image.Tag == cover.Tag) //Provjera da li je polje zatvoreno, ako jeste izvršavanje narednog koda, ako nije prelazak na naredno polje
                            {
                                drugaOtvorena.Image = (Image)slikaPcb[drugaOtvorena]; //Postavljanje slike koja odgovara polju u Hash mapi u polje umjesto cover slike
                                System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u otvaranju polja
                                drugaOtvorena.Update(); //Prikaz izmijenjene verzije polja

                                if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag) //Ispitivanje da li su polja u varijablama prvaOtvorena i drugaOtvorena par po pripadajućim slikama
                                {
                                    drugaOtvorena.Image = Properties.Resources.pozadina4; //Ako polje u drugaOtvorena nije traženi par zatvaranje polja, tj. postavljanje slike pozadine u polje
                                    drugaOtvorena.Image.Tag = cover.Tag; //Postavljanje odgovarajućeg taga slici na osnovu koga se provjerava da li je polje zatvoreno
                                    System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u zatvaranju polja
                                    drugaOtvorena.Update(); //Prikaz izmijenjene verzije polja
                                }
                            }
                            j++; //Uvećanje za 1 indeksa koji služi za prolazak kroz polja kvadranta u cilju traženja para polju sa indeksom i
                        }

                        if (drugaOtvorena == null) //Prekid petlje ako nema polja smještenog u drugaOtvorena
                            break;
                    }
                    while (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag && j < pocetnoPolje + velicina * velicinaKvadranta); //Definisanje uslova za prekid petlje, ako je pronađen par ili ako se došlo do posljednjeg polja u kvadrantu

                    if (drugaOtvorena == null || drugaOtvorena.Image.Tag !=prvaOtvorena.Image.Tag) //Ako nema polja dodijeljenog varijabli drugaOtvorena ili ako nije pronađen par polju iz varijable prvaOtvorena zatvaranje polja u prvaOtvorena
                    {
                        prvaOtvorena.Image = cover; //Zatvaranje polja prvaOtvorena, tj. postavljanje slike cover u polje
                        System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u zatvaranju polja
                        prvaOtvorena.Update(); //Prikaz izmijenjene verzije polja
                    }
                }

                if (i % velicinaKvadranta == (velicinaKvadranta - 1)) //Ako se dođe do kraja reda u datom kvadrantu prelazak na prvo polje u narednom redu tog kvadranta
                    i += velicinaKvadranta;
            }
        }
        private void otvaranjeCijelogPolja() //Funckija za otvaranje svih polja sekvencijalno dok se ne nađe svakom polju par po slici
        {
            PictureBox.CheckForIllegalCrossThreadCalls = false; //Omogućavanje pristupa PictureBox-ovima na formi iz različitih niti

            for (int i = 0; i < pnlSlike.Controls.Count; i++) //Prolazak kroz sva polja na panelu
            {
                PictureBox prvaOtvorena = (PictureBox)pnlSlike.Controls[i]; //Smještanje trenutnog polja u varijablu prvaOtvorena
                if (prvaOtvorena.Image.Tag == cover.Tag) //Provjeravanje da li je polje smješteno u prvaOtvorena zatvoreno, ako je zatvoreno izvršavanje narednog koda, ako nije ignorisanje polja i prelazak na naredno
                {
                    prvaOtvorena.Image = (Image)slikaPcb[prvaOtvorena]; //Postavljanje slike koja odgovara polju u Hash mapi kao slike trenutnog pictureBox-a umjesto cover slike
                    System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u otvaranju polja
                    prvaOtvorena.Update(); //Prikaz izmijenjene verzije polja

                    int j = (i + 1); //Uzimanje indeksa koji je sljedići u nizu iza indeksa trenutnog polja
                    PictureBox drugaOtvorena = null; //Kreiranje varijable drugaOtvorena tipa PictureBox za smještanje polja prilikom traženja para polju iz varijable prvaOtvorena
                    do //Prolazak kroz sva polja dok se ne nađe odgovarajući par
                    {
                        drugaOtvorena = (PictureBox)pnlSlike.Controls[j]; //Smještanje polja sa indeksom j u varijablu drugaOtvorena
                        if (drugaOtvorena.Image.Tag == cover.Tag) //Provjera da li je polje zatvoreno, ako jeste izvršavanje narednog koda, ako nije prelazak na naredno polje
                        {
                            drugaOtvorena.Image = (Image)slikaPcb[drugaOtvorena]; //Postavljanje slike koja odgovara polju u Hash mapi u polje umjesto cover slike
                            System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u otvaranju polja
                            drugaOtvorena.Update(); //Prikaz izmijenjene verzije polja

                            if (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag) //Ispitivanje da li su polja u varijablama prvaOtvorena i drugaOtvorena par po pripadajućim slikama
                            {
                                drugaOtvorena.Image = cover; //Ako polje u drugaOtvorena nije traženi par zatvaranje polja, tj. postavljanje slike cover u polje
                                System.Threading.Thread.Sleep(1000); //Pauza u izvršavanju koja omogućava korisniku da vidi korake u zatvaranju polja
                                drugaOtvorena.Update(); //Prikaz izmijenjene verzije polja
                            }
                        }
                        j++; //Uvećanje za 1 indeksa koji služi za prolazak kroz polja iza polja sa indeksom 1 u cilju traženja njegovog para

                    }

                    while (drugaOtvorena.Image.Tag != prvaOtvorena.Image.Tag && j < pnlSlike.Controls.Count); //Definisanje uslova za prekid petlje, ako je pronađen par ili ako se došlo do posljednjeg polja na panelu
                }
            }
        }

        private void frmMemorija_FormClosed(object sender, FormClosedEventArgs e) //Funkcija koja se poziva prilikom zatvaranja forme
        {
            Application.Exit(); //Zatvaranje cijele aplikacije prilikom zatvaranja glavne forme
        }

        private void btnBez_Click(object sender, EventArgs e) //Funkcija koja se poziva prilikom klika na dugme koje označava izvršenje simulacije bez paralelizacije
        {
            timer = new System.Windows.Forms.Timer(); //Kreiranje nove instance tajmera koji mjeri vrijeme izvršavanja simulacije koja se pokreće
            timer.Interval = 1000; //Postavljanje intervala tajmera na 1000 ms, tj. da tajmer otkucava svake sekunde
            timer.Enabled = true; //Omogućavanje rada tajmera
            timer.Tick += Timer_Tick; //Postavljanje funkcije Timer_Tick kao funkcije koja će se pozivati prilikom svakog otkucaja tajmera

            t1 = new Thread(new ThreadStart(delegate { otvaranjeCijelogPolja(); timer.Stop(); })); //Pozivanje funkcije otvaranjeCijelogPolja i stopiranje tajmera nakon povratka iz funkcije u okviru nove niti koja se kreira i dodaje varijabli t1 tipa Thread
            t1.Start(); //Pokretanje izvršavanja niti smještene u t1
        }
    }
}
