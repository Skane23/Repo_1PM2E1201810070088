using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E1201810070088
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {

            //await Navigation.PopAsync(true);
            base.OnBackButtonPressed();
            return true;

        }


        private async void ingresar_Clicked(object sender, EventArgs e)
        {

            if(String.IsNullOrWhiteSpace(login.Text) == true)
            {
                await DisplayAlert("Alerta", "nombre vacio", "Ok");

            }
            else
            {
                if (String.IsNullOrWhiteSpace(pass.Text) == true)
                {
                    await DisplayAlert("Alerta", "pass vacio", "Ok");

                }
                else
                {
                    Preferences.Set("users", login.Text);
                    Preferences.Set("pass", pass.Text);
                    Preferences.Set("Remember", false);

                    await Navigation.PushAsync(new DashBoard());
                }
            }
             
        }

        private async void registrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrarse());
        }
    }
}
