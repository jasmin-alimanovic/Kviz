using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kviz.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Glavna : ContentPage
    {


        public Glavna()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pitanja());
        }
    }
}