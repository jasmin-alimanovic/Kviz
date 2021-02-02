using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kviz.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RezultatPage : ContentPage
    {
        public RezultatPage(string rezultat, string poruka)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            ButtonOsvojeno.Text = rezultat + " BAM";
            NaslovLabel.Text = poruka;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}