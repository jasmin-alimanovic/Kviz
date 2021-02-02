using Kviz.Model;
using Kviz.View;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kviz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pitanja : ContentPage
    {
        private int i;
        private readonly string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "kviz_db.db3");
        public int PID;
        public int[] Nagrade;
        public IList<Pitanje> Pit { get; private set; }
        public IList<Odgovor> Odg { get; private set; }
        public int zagarantovano;
        public uint duzinaAnim;
        public uint AnimY;
        int osvojeno;
        public Pitanja()
        {

            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            i = 0;
            duzinaAnim = 1000;
            Nagrade = new int[15] { 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 };
            zagarantovano = 0;
            AnimY = 500;
            osvojeno = 0;
            StackRezultat.IsVisible = false;
            this.UnosPodataka();
            Pit = new List<Pitanje>();
            Odg = new List<Odgovor>();
            var db = new SQLiteConnection(_dbPath);
            Pit = db.Table<Pitanje>().ToList();
            List<Pitanje> lists = Pit.OrderBy(x => Guid.NewGuid()).ToList();
            Pit = lists;
            PID = Pit[i].Id;
            Odg = db.Table<Odgovor>().Where(x => x.PitanjeID == PID).ToList();
            TekstPitanja.Text = Pit[i].Tekst;
            Odg1.Text = Odg[0].TekstOdg;
            Odg2.Text = Odg[1].TekstOdg;
            Odg3.Text = Odg[2].TekstOdg;
            Odg4.Text = Odg[3].TekstOdg;

        }

        private async void Odg1_Clicked(object sender, EventArgs e)
        {
            if (Odg[0].IsTacan)
            {
                osvojeno = Nagrade[i];


                if (i == 14)
                {
                    Odg1.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg1.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg1.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg1.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg1.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg1.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg1.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg1.BackgroundColor = Color.LightGray;
                    await Task.Delay(1000);
                    string poruka = "Čestitamo! Osvojili ste:";
                    RezultatPage page = new RezultatPage(osvojeno.ToString(), poruka);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    var db = new SQLiteConnection(_dbPath);
                    int sljedeci = i + 1;
                    PID = Pit[sljedeci].Id;

                    Odg = db.Table<Odgovor>().Where(x => x.PitanjeID == PID).ToList();


                    if (Nagrade[i] == 1000 || Nagrade[i] == 32000)
                    {
                        zagarantovano = Nagrade[i];
                        Odg1.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg1.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg1.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg1.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Zagarantovano";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;
                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    else
                    {
                        //await DisplayAlert(null, $"Tacan odgovor!\nOsvojeno {osvojeno} BAM", "OK");
                        Odg1.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg1.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg1.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg1.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Osvojeno";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;

                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    i++;

                }
            }
            else
            {
                //await DisplayAlert(null, "Netacan odgovor!", "OK");
                Odg1.BackgroundColor = Color.Red;
                await Task.Delay(1000);
                Odg1.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg1.BackgroundColor = Color.Red;
                await Task.Delay(700);
                Odg1.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg1.BackgroundColor = Color.Red;
                await Task.Delay(500);
                Odg1.BackgroundColor = Color.White;
                await Task.Delay(1000);
                string poruka = (zagarantovano == 0) ? "Nažalost, niste ništa osvojili." : "Čestitamo! Osvojili ste:";
                RezultatPage page = new RezultatPage(zagarantovano.ToString(), poruka);
                await Navigation.PushAsync(page);
            }

        }

        private async void Odg2_Clicked(object sender, EventArgs e)
        {
            if (Odg[1].IsTacan)
            {
                osvojeno = Nagrade[i];


                if (i == 14)
                {
                    Odg2.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg2.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg2.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg2.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg2.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg2.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg2.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg2.BackgroundColor = Color.LightGray;
                    await Task.Delay(1000);
                    string poruka = "Čestitamo! Osvojili ste:";
                    RezultatPage page = new RezultatPage(osvojeno.ToString(), poruka);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    var db = new SQLiteConnection(_dbPath);
                    int sljedeci = i + 1;
                    PID = Pit[sljedeci].Id;

                    Odg = db.Table<Odgovor>().Where(x => x.PitanjeID == PID).ToList();

                    if (Nagrade[i] == 1000 || Nagrade[i] == 32000)
                    {
                        zagarantovano = Nagrade[i];
                        Odg2.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg2.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg2.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg2.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Zagarantovano";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;
                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    else
                    {
                        //await DisplayAlert(null, $"Tacan odgovor!\nOsvojeno {osvojeno} BAM", "OK");
                        Odg2.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg2.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg2.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg2.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Osvojeno";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;

                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    i++;

                }
            }
            else
            {
                //await DisplayAlert(null, "Netacan odgovor!", "OK");
                Odg2.BackgroundColor = Color.Red;
                await Task.Delay(1000);
                Odg2.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg2.BackgroundColor = Color.Red;
                await Task.Delay(700);
                Odg2.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg2.BackgroundColor = Color.Red;
                await Task.Delay(500);
                Odg2.BackgroundColor = Color.White;
                await Task.Delay(1000);
                string poruka = (zagarantovano == 0) ? "Nažalost, niste ništa osvojili." : "Čestitamo! Osvojili ste:";
                RezultatPage page = new RezultatPage(zagarantovano.ToString(), poruka);
                await Navigation.PushAsync(page);
            }
        }

        private async void Odg3_Clicked(object sender, EventArgs e)
        {
            if (Odg[2].IsTacan)
            {
                osvojeno = Nagrade[i];


                if (i == 14)
                {
                    Odg3.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg3.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg3.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg3.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg3.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg3.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg3.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg3.BackgroundColor = Color.LightGray;
                    await Task.Delay(1000);
                    string poruka = "Čestitamo! Osvojili ste:";
                    RezultatPage page = new RezultatPage(osvojeno.ToString(), poruka);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    var db = new SQLiteConnection(_dbPath);
                    int sljedeci = i + 1;
                    PID = Pit[sljedeci].Id;

                    Odg = db.Table<Odgovor>().Where(x => x.PitanjeID == PID).ToList();


                    if (Nagrade[i] == 1000 || Nagrade[i] == 32000)
                    {
                        zagarantovano = Nagrade[i];
                        Odg3.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg3.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg3.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg3.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Zagarantovano";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;
                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    else
                    {
                        //await DisplayAlert(null, $"Tacan odgovor!\nOsvojeno {osvojeno} BAM", "OK");
                        Odg3.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg3.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg3.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg3.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Osvojeno";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;

                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    i++;

                }
            }
            else
            {
                //await DisplayAlert(null, "Netacan odgovor!", "OK");
                Odg3.BackgroundColor = Color.Red;
                await Task.Delay(1000);
                Odg3.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg3.BackgroundColor = Color.Red;
                await Task.Delay(700);
                Odg3.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg3.BackgroundColor = Color.Red;
                await Task.Delay(500);
                Odg3.BackgroundColor = Color.White;
                await Task.Delay(1000);
                string poruka = (zagarantovano == 0) ? "Nažalost, niste ništa osvojili." : "Čestitamo! Osvojili ste:";
                RezultatPage page = new RezultatPage(zagarantovano.ToString(), poruka);
                await Navigation.PushAsync(page);
            }
        }

        private async void Odg4_Clicked(object sender, EventArgs e)
        {
            if (Odg[3].IsTacan)
            {
                osvojeno = Nagrade[i];


                if (i == 14)
                {
                    Odg4.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg4.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg4.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg4.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg4.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg4.BackgroundColor = Color.LightGray;
                    await Task.Delay(200);
                    Odg4.BackgroundColor = Color.Green;
                    await Task.Delay(500);
                    Odg4.BackgroundColor = Color.LightGray;
                    await Task.Delay(1000);
                    string poruka = "Čestitamo! Osvojili ste:";
                    RezultatPage page = new RezultatPage(osvojeno.ToString(), poruka);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    var db = new SQLiteConnection(_dbPath);
                    int sljedeci = i + 1;
                    PID = Pit[sljedeci].Id;

                    Odg = db.Table<Odgovor>().Where(x => x.PitanjeID == PID).ToList();


                    if (Nagrade[i] == 1000 || Nagrade[i] == 32000)
                    {
                        zagarantovano = Nagrade[i];
                        Odg4.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg4.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg4.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg4.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Zagarantovano";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;
                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    else
                    {
                        //await DisplayAlert(null, $"Tacan odgovor!\nOsvojeno {osvojeno} BAM", "OK");
                        Odg4.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg4.BackgroundColor = Color.LightGray;
                        await Task.Delay(200);
                        Odg4.BackgroundColor = Color.Green;
                        await Task.Delay(500);
                        Odg4.BackgroundColor = Color.LightGray;
                        await Task.Delay(500);
                        await PitanjeBlok.TranslateTo(0, AnimY, duzinaAnim, Easing.Linear);
                        TekstPitanja.Text = Pit[sljedeci].Tekst;
                        Odg1.Text = Odg[0].TekstOdg;
                        Odg2.Text = Odg[1].TekstOdg;
                        Odg3.Text = Odg[2].TekstOdg;
                        Odg4.Text = Odg[3].TekstOdg;
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = true;
                        ButtonOsvojeno.Text = Nagrade[i].ToString();
                        NaslovLabel.Text = "Osvojeno";
                        await StackRezultat.TranslateTo(0, 0, 1000, Easing.Linear);
                        await Task.Delay(1000);
                        await StackRezultat.TranslateTo(0, -300, 500, Easing.Linear);
                        StackRezultat.IsVisible = false;

                        await PitanjeBlok.TranslateTo(0, 0, AnimY, Easing.Linear);
                    }
                    i++;

                }
            }
            else
            {
                //await DisplayAlert(null, "Netacan odgovor!", "OK");
                Odg4.BackgroundColor = Color.Red;
                await Task.Delay(1000);
                Odg4.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg4.BackgroundColor = Color.Red;
                await Task.Delay(700);
                Odg4.BackgroundColor = Color.White;
                await Task.Delay(200);
                Odg4.BackgroundColor = Color.Red;
                await Task.Delay(500);
                Odg4.BackgroundColor = Color.White;
                await Task.Delay(1000);
                string poruka = (zagarantovano == 0) ? "Nažalost, niste ništa osvojili." : "Čestitamo! Osvojili ste:";
                RezultatPage page = new RezultatPage(zagarantovano.ToString(), poruka);
                await Navigation.PushAsync(page);
            }
        }

        private void UnosPodataka()
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Pitanje>();
            db.CreateTable<Odgovor>();
            if (db.Table<Pitanje>().Count() == 0 || db.Table<Odgovor>().Count() == 0)
            {
                IList<Pitanje> P = new List<Pitanje>();
                //Dodavanje 1. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Najveći kontinent na svijetu?"
                });
                //Dodavanje 2. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koliki je zbir uglova u trouglu?"
                }); //360 180 420 120
                //Dodavanje 3. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koje životinje ima više nego ljudi?"
                }); // Slonova Mrava Krokodila Nosoroga
                //Dodavanje 4. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Najtrofejniji engleski fudbalski klub je?"
                }); // Liverpool(T) Everton ManUtd Chelsea
                //Dodavanje 5. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Ko je 395. godine razdijelio Rimsko carstvo na zapadno i istočno?"
                }); // Heraklije Justinijan Konstanti VII Teodozije(1)
                //Dodavanje 6. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Ko je bio prvi čovjek koji je nogom stupio na sve kontinente osim Antarktike?"
                }); //James Cook(T), Filip IV, Christopher Columbo, Magellan

                //Dodavanje 7. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koji je najduži planinski lanac na Zemlji?"
                }); // Himalaji, Kavkasko gorje, Alpe, Ande(T)

                //Dodavanje 8. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Ko je grčki bog rata, simbol hrabrosti, silovitosti i junačke snage?"
                }); //Hefest, Had, Posejdon, Ares(T)

                //Dodavanje 9. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koja od sljedećih tvrdji je tačna?"
                });

                //Dodavanje 10. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Mirmekologija je nauka o čemu?"
                }); //Mravima(T), Leptirima, Bubama,  Pčelama

                //Dodavanje 11. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Prevedeno na bosanski jezik, \"Veni, vidi, vici\", je u kojem glagolskom vremenu?"
                });

                //Dodavanje 12. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koliko je minuta Jurij Gagarin proveo u svemiru na svom prvom letu oko Zemlje?"
                });//99, 35, 302, 108(T)

                //Dodavanje 13. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koji je peti kontinent po veličini?"
                }); //Antarktik(T), Azija, Afrika, Europa

                //Dodavanje 14. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Od koliko mišića se sastoji ljudsko tijelo?"
                }); // 798, 752(T), 892, 812

                //Dodavanje 15. pitanja
                P.Add(new Pitanje
                {
                    Tekst = "Koje mjerne jedinice NEMA u Ohmovom trokutu"
                }); //I U R W(T)

                //insert pitanja u bazu
                db.InsertAll(P);


                IList<Pitanje> pitanja = new List<Pitanje>();
                pitanja = db.Table<Pitanje>().OrderBy(x => x.Id).ToList();

                IList<Odgovor> O = new List<Odgovor>();

                //PITANJE 1
                //Pitanje 1 odgovor 1
                int Pid = pitanja[0].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Europa",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 1 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Afrika",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 1 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Antarktik",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 1 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Azija",
                    IsTacan = true,
                    PitanjeID = Pid
                });

                //PITANJE 2
                //Pitanje 2 odgovor 1
                Pid = pitanja[1].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "360",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 2 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "180",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 2 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "420",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 2 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "120",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //PITANJE 3
                //Pitanje 3 odgovor 1
                Pid = pitanja[2].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Slonova",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 3 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Mrava",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 3 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Krokodila",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 3 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Nosoroga",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //PITANJE 4
                //Pitanje 4 odgovor 1
                Pid = pitanja[3].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Liverpool",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 4 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Everton",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 4 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Man Utd",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 4 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Chelsea",
                    IsTacan = false,
                    PitanjeID = Pid
                });


                //PITANJE 5 
                //Pitanje 5 odgovor 1
                Pid = pitanja[4].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Heraklije",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 5 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Justinijan",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 5 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Konstantin",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 5 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Teodozije",
                    IsTacan = true,
                    PitanjeID = Pid
                });

                //PITANJE 6  James Cook(T), Filip IV, Christopher Columbo, Magellan
                //Pitanje 6 odgovor 1 
                Pid = pitanja[5].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "James Cook",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 6 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Filip IV",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 6 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Christopher Columbo",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 6 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Magellan",
                    IsTacan = false,
                    PitanjeID = Pid
                });

                //PITANJE 7  James Cook(T), Filip IV, Christopher Columbo, Magellan
                //Pitanje 7 odgovor 1  
                Pid = pitanja[6].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Himalaji",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 7 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Kavkasko gorje",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 7 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Alpe",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 7 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Ande",
                    IsTacan = true,
                    PitanjeID = Pid
                });

                //PITANJE 8  
                //Pitanje 8 odgovor 1  Hefest, Had, Posejdon, Ares(T)
                Pid = pitanja[7].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Hefest",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 8 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Had",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 8 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Posejdon",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 8 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Ares",
                    IsTacan = true,
                    PitanjeID = Pid
                });

                //PITANJE 9  
                //Pitanje 9 odgovor 1  
                Pid = pitanja[8].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Argentina je veća od Indije",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 9 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Turska je veća od Egipta",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 9 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Brazil je veći Australije",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 9 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Indija je veca od Kine",
                    IsTacan = false,
                    PitanjeID = Pid
                });

                //PITANJE 10  
                //Pitanje 10 odgovor 1  
                Pid = pitanja[9].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Mravima",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 10 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Leptirima",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 10 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Bubama",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 10 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Pčelama",
                    IsTacan = false,
                    PitanjeID = Pid
                });

                //PITANJE 11  
                //Pitanje 11 odgovor 1  
                Pid = pitanja[10].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Perfekt",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 11 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Aorist",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 11 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Pluskvamperfekt",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 11 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Imperfekt",
                    IsTacan = false,
                    PitanjeID = Pid
                });

                //PITANJE 12  
                //Pitanje 12 odgovor 1  99, 35, 302, 108(T)
                Pid = pitanja[11].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "99",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 12 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "35",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 12 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "302",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 12 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "108",
                    IsTacan = true,
                    PitanjeID = Pid
                });

                //PITANJE 13  
                //Pitanje 13 odgovor 1  
                Pid = pitanja[12].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "Antarktik",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 13 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "Azija",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 13 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "Afrika",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 13 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "Europa",
                    IsTacan = false,
                    PitanjeID = Pid
                });

                //PITANJE 14  
                //Pitanje 14 odgovor 1  
                Pid = pitanja[13].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "798",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 14 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "752",
                    IsTacan = true,
                    PitanjeID = Pid
                });
                //Pitanje 14 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "892",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 14 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "812",
                    IsTacan = false,
                    PitanjeID = Pid
                });

                //PITANJE 15  
                //Pitanje 15 odgovor 1  I U R W(T)
                Pid = pitanja[14].Id;
                O.Add(new Odgovor
                {
                    TekstOdg = "I",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 15 odgovor 2
                O.Add(new Odgovor
                {
                    TekstOdg = "U",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 15 odgovor 3
                O.Add(new Odgovor
                {
                    TekstOdg = "R",
                    IsTacan = false,
                    PitanjeID = Pid
                });
                //Pitanje 15 odgovor 4
                O.Add(new Odgovor
                {
                    TekstOdg = "W",
                    IsTacan = true,
                    PitanjeID = Pid
                });

                db.InsertAll(O);
            }
        }

        // dzokeri tapped

        private async void PublikaTap_Tapped(object sender, EventArgs e)
        {
            string Prikaz = "";
            if (Odg[0].IsTacan)
            {
                if (i <= 5)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 100%\n{Odg[1].TekstOdg}: 0%\n{Odg[2].TekstOdg}: 0%\n{Odg[3].TekstOdg}: 0%\n";
                }
                else if (i > 5 && i <= 10)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 66%\n{Odg[1].TekstOdg}: 4%\n{Odg[2].TekstOdg}: 23%\n{Odg[3].TekstOdg}: 7%\n";
                }
                else
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 35%\n{Odg[1].TekstOdg}: 12%\n{Odg[2].TekstOdg}: 20%\n{Odg[3].TekstOdg}: 33%\n";
                }
            }
            else if (Odg[1].IsTacan)
            {
                if (i <= 5)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 0%\n{Odg[1].TekstOdg}: 100%\n{Odg[2].TekstOdg}: 0%\n{Odg[3].TekstOdg}: 0%\n";
                }
                else if (i > 5 && i <= 10)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 4%\n{Odg[1].TekstOdg}: 66%\n{Odg[2].TekstOdg}: 23%\n{Odg[3].TekstOdg}: 7%\n";
                }
                else
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 12%\n{Odg[1].TekstOdg}: 35%\n{Odg[2].TekstOdg}: 20%\n{Odg[3].TekstOdg}: 33%\n";
                }
            }
            else if (Odg[2].IsTacan)
            {
                if (i <= 5)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 0%\n{Odg[1].TekstOdg}: 0%\n{Odg[2].TekstOdg}: 100%\n{Odg[3].TekstOdg}: 0%\n";
                }
                else if (i > 5 && i <= 10)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 4%\n{Odg[1].TekstOdg}: 23%\n{Odg[2].TekstOdg}: 66%\n{Odg[3].TekstOdg}: 7%\n";
                }
                else
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 12%\n{Odg[1].TekstOdg}: 20%\n{Odg[2].TekstOdg}: 35%\n{Odg[3].TekstOdg}: 33%\n";
                }
            }
            else if (Odg[3].IsTacan)
            {
                if (i <= 5)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 0%\n{Odg[1].TekstOdg}: 0%\n{Odg[2].TekstOdg}: 0%\n{Odg[3].TekstOdg}: 100%\n";
                }
                else if (i > 5 && i <= 10)
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 4%\n{Odg[1].TekstOdg}: 23%\n{Odg[2].TekstOdg}: 7%\n{Odg[3].TekstOdg}: 66%\n";
                }
                else
                {
                    Prikaz += $"{Odg[0].TekstOdg}: 12%\n{Odg[1].TekstOdg}: 20%\n{Odg[2].TekstOdg}: 33%\n{Odg[3].TekstOdg}: 35%\n";
                }
            }
            await DisplayAlert("Pomoć publike", Prikaz, "OK");
            PublikaSlika.IsEnabled = false;
        }

        private void PolaPolaTap_Tapped(object sender, EventArgs e)
        {
            if (Odg[0].IsTacan)
            {
                int rand = getRandom(0);
                if (rand != 1)
                {
                    Odg2.Text = "";
                }
                if (rand != 2)
                {
                    Odg3.Text = "";
                }
                if (rand != 3)
                {
                    Odg4.Text = "";
                }
            }
            else if (Odg[1].IsTacan)
            {
                int rand = getRandom(1);
                if (rand != 0)
                {
                    Odg1.Text = "";
                }
                if (rand != 2)
                {
                    Odg3.Text = "";
                }
                if (rand != 3)
                {
                    Odg4.Text = "";
                }
            }
            else if (Odg[2].IsTacan)
            {
                int rand = getRandom(2);
                if (rand != 0)
                {
                    Odg1.Text = "";
                }
                if (rand != 1)
                {
                    Odg2.Text = "";
                }
                if (rand != 3)
                {
                    Odg4.Text = "";
                }
            }
            else if (Odg[3].IsTacan)
            {
                int rand = getRandom(3);
                if (rand != 0)
                {
                    Odg1.Text = "";
                }
                if (rand != 2)
                {
                    Odg3.Text = "";
                }
                if (rand != 1)
                {
                    Odg2.Text = "";
                }
            }
            PolaPolaSlika.IsEnabled = false;
        }

        private async void PhoneTap_Tapped(object sender, EventArgs e)
        {

            if (Odg[0].IsTacan)
            {
                var rand = new Random().Next(50) + 51;
                await PopupNavigation.Instance.PushAsync(new PopUpPoziv(Pit[i].Tekst, Odg[0].TekstOdg, Odg[1].TekstOdg, Odg[2].TekstOdg, Odg[3].TekstOdg, Odg[0].TekstOdg, rand), true);
            }
            else if (Odg[1].IsTacan)
            {
                var rand = new Random().Next(50) + 51;
                await PopupNavigation.Instance.PushAsync(new PopUpPoziv(Pit[i].Tekst, Odg[0].TekstOdg, Odg[1].TekstOdg, Odg[2].TekstOdg, Odg[3].TekstOdg, Odg[1].TekstOdg, rand), true);
            }
            else if (Odg[2].IsTacan)
            {
                var rand = new Random().Next(50) + 51;
                await PopupNavigation.Instance.PushAsync(new PopUpPoziv(Pit[i].Tekst, Odg[0].TekstOdg, Odg[1].TekstOdg, Odg[2].TekstOdg, Odg[3].TekstOdg, Odg[2].TekstOdg, rand), true);
            }
            else if (Odg[3].IsTacan)
            {
                var rand = new Random().Next(50) + 51;
                await PopupNavigation.Instance.PushAsync(new PopUpPoziv(Pit[i].Tekst, Odg[0].TekstOdg, Odg[1].TekstOdg, Odg[2].TekstOdg, Odg[3].TekstOdg, Odg[3].TekstOdg, rand), true);
            }
            PhoneSlika.IsEnabled = false;
        }

        private int getRandom(int broj)
        {
            Random R = new Random();
            int rand;
            do
            {
                rand = R.Next(4);
            } while (rand == broj);

            return rand;
        }
    }
}