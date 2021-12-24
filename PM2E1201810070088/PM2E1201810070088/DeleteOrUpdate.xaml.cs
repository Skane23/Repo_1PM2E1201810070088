using Plugin.Media;
using PM2E1201810070088.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1201810070088
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteOrUpdate : ContentPage
    {
        string base64Val = "";
        CasasPoint a;
        public DeleteOrUpdate(CasasPoint dato )
        {
            InitializeComponent();
            lblID.Text = dato.codigo+"";
            describir.Text = dato.descripcion;
            direccion.Text = dato.direccion;
            a = dato;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Datos_Ubicacion();
        }

        public async void Datos_Ubicacion()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                    lbllatitud.Text = "00.0";
                    lbllongitud.Text = "00.0";
                }


                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    lbllatitud.Text = location.Latitude.ToString();
                    lbllongitud.Text = location.Longitude.ToString();
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
            catch (Exception)
            {
                // Unable to get location
            }
        }


        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Mapas(a));
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                }
                else
                {

                    var casas = new Modelos.CasasPoint
                    {
                        codigo = Convert.ToInt32(lblID.Text),
                        latitud = Convert.ToDouble(lbllatitud.Text),
                        longitud = Convert.ToDouble(lbllatitud.Text),
                        descripcion = describir.Text,
                        direccion = direccion.Text,
                        base64 = base64Val
                    };

                    var resultado = await App.BaseDatos.EliminarCasasPoint(casas);

                    if (resultado == 1)
                    {
                        await DisplayAlert("Mensaje", "Eliminado exitoso!!!", "ok");
                        await Navigation.PushAsync(new ListadoCasas());
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo Eliminado", "ok");
                    }

                }

                /*
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    txtLatitud.Text = location.Latitude.ToString(); 
                }
                */
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
            catch (Exception)
            {
                // Unable to get location
            }
        }

        private async void btnModif_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(direccion.Text) == true)
                    {
                        await DisplayAlert("Error", "Debe escribir la direccion de la casa ", "Ok");
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(describir.Text) == true)
                        {
                            await DisplayAlert("Error", "Debe describir la direccion de casa", "Ok");
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(base64Val) == true)
                            {
                                await DisplayAlert("Error", "Ingrese la foto", "Ok");
                            }
                            else
                            {
                                Guardar_Datos();
                            }

                        }

                    }


                }

                /*
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    txtLatitud.Text = location.Latitude.ToString(); 
                }
                */
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
            catch (Exception)
            {
                // Unable to get location
            }

        }

        private async void btncargarimg_Clicked(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                imagen.Source = ImageSource.FromStream(() => { return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64Val = Convert.ToBase64String(imagenByte);
                //await EmpleController.SubirImagen(imagenByte);
            }
        }


        public async void Guardar_Datos()
        {
            var casas = new Modelos.CasasPoint
            {
                codigo = Convert.ToInt32(lblID.Text),
                latitud = Convert.ToDouble(lbllatitud.Text),
                longitud = Convert.ToDouble(lbllatitud.Text),
                descripcion = describir.Text,
                direccion = direccion.Text,
                base64 = base64Val
            };

            var resultado = await App.BaseDatos.GrabarCasasPoint(casas);

            if (resultado == 1)
            {
                await DisplayAlert("Mensaje", "Registro exitoso!!!", "ok");
                 
                //Datos_Ubicacion();
                await Navigation.PushAsync(new ListadoCasas());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Modificos", "ok");
            }
        }

        

    }
}