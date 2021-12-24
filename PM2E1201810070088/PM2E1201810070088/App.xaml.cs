using PM2E1201810070088.Controller;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1201810070088
{
    public partial class App : Application
    {
        static DataBaseSQLite basedatos;
        public static DataBaseSQLite BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new DataBaseSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recuperacion.db3"));
                }


                return basedatos;
            }

        }


        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());

            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new DashBoard());
            }
        }

        protected override void OnStart()
        {

            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new DashBoard());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
