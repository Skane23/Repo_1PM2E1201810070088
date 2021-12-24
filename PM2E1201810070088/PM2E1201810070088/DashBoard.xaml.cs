using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1201810070088
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoard : ContentPage
    {
        public DashBoard()
        {
            InitializeComponent();
            string name = Preferences.Get("users", "");
            string pass = Preferences.Get("pass", "");
            lbl.Text = "Hola " + name + " Tu pass es: " + pass;
        }

        private async void btncerrar_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("users");
            Preferences.Remove("pass");
            Preferences.Remove("Remember");
            Preferences.Set("Remember", true);
            //Preferences.Clear();



            await Application.Current.MainPage.Navigation.PopAsync();
            await Navigation.PushAsync(new MainPage());
        }
    }
}