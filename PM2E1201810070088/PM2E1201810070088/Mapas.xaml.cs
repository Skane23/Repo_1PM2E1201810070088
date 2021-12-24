using PM2E1201810070088.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E1201810070088
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapas : ContentPage
    {
        public Mapas(CasasPoint dato )
        {
            InitializeComponent();


            Position position = new Position(dato.latitud, dato.longitud);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map(mapSpan);

            Pin pin = new Pin
            {
                Label = dato.descripcion,
                Address = dato.direccion,
                Type = PinType.Place,
                Position = position
            };
            map.Pins.Add(pin);

            Content = map;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    Title = "GPS Inactivo";

                }
                else
                {
                    Title = "GPS Activo";
                }



            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (System.Exception)
            {
                // Unable to get location
            }
        }



    }
}