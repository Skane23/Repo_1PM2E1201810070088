using PM2E1201810070088.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1201810070088
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoCasas : ContentPage
    {
        public ListadoCasas()
        {
            InitializeComponent();
            Title = "Listado";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarListado();
        }

        public async void cargarListado()
        {
            var lista = await App.BaseDatos.ObtenerListaCasasPoint();
            listaCasaPoin.ItemsSource = lista;
        }

        private async void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //var item = (Localizacion)e.SelectedItem;
                CasasPoint local = (CasasPoint)e.SelectedItem;

                //await DisplayAlert("s", l.codigo.ToString(), "ok");
                DeleteOrUpdate ventana = new DeleteOrUpdate(local);
                await Navigation.PushAsync(ventana);


                listaCasaPoin.SelectedItem = null;

            }
        }
    }
}