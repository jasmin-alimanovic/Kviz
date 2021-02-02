using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace Kviz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPoziv : PopupPage
    {
        private readonly string p;
        private readonly string o1;
        private readonly string o2;
        private readonly string o3;
        private readonly string o4;
        private readonly string tOdg;
        private readonly int procenat;
        public PopUpPoziv(string p, string o1, string o2, string o3, string o4, string tOdg, int procenat)
        {
            InitializeComponent();
            this.tOdg = tOdg;
            this.o1 = o1;
            this.o2 = o2;
            this.o3 = o3;
            this.o4 = o4;
            this.p = p;
            this.procenat = procenat;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            lblPozivTekst.Text += "Ja: Hej, šta ima? Treba mi pomoć oko jednog pitanja na Milijunašu.";
            await Task.Delay(1500);
            lblPozivTekst.Text += $"\n\nPrijatelj: Nema ništa, reci.";
            await Task.Delay(1500);
            lblPozivTekst.Text += $"\n\nJa:\t{p}\n";
            await Task.Delay(1000);
            lblPozivTekst.Text += $"A: {o1}\n";
            await Task.Delay(1000);
            lblPozivTekst.Text += $"B: {o2}\n";
            await Task.Delay(1000);
            lblPozivTekst.Text += $"C: {o3}\n";
            await Task.Delay(1000);
            lblPozivTekst.Text += $"D: {o4}\n";
            await Task.Delay(2000);
            lblPozivTekst.Text += $"Prijatelj: Mislim da je {tOdg} tačan odgovor.\n";
            await Task.Delay(1000);
            lblPozivTekst.Text += $"Ja: Koliko si siguran?\n";
            await Task.Delay(1500);
            lblPozivTekst.Text += $"Prijatelj: Siguran sam {procenat}%.\n";
            await Task.Delay(1000);
            lblPozivTekst.Text += $"Ja: Hvala ti puno.";
        }
    }
}