using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using XamiTrips.Clases;
using Android.Content;
using System.Collections.Generic;
using XamiTrips.Clases.Properties;
using SQLite;

namespace XamiTrips
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText nombreUsuario, passwordUsuario;
        Button inicioSesion;
        TextView crearCuenta;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            nombreUsuario = FindViewById<EditText>(Resource.Id.NombreEditText);
            passwordUsuario = FindViewById<EditText>(Resource.Id.ContraseñaEditText);
            inicioSesion = FindViewById<Button>(Resource.Id.SesionButton);
            crearCuenta = FindViewById<TextView>(Resource.Id.createAccountText);
            inicioSesion.Click += InicioSesion_Click;
            crearCuenta.Click += CrearCuenta_Click;            
        }

        private void CrearCuenta_Click(object sender, EventArgs e)
        {
            Intent intentAccount = new Intent(this, typeof(CrearCuentaActivity));
            StartActivity(intentAccount);
        }

        private void InicioSesion_Click(object sender, EventArgs e)
        {

            if (InicioSesion.ValidarSesionDb(nombreUsuario.Text, passwordUsuario.Text))
            {

                List<Viaje> lista = DataBase.ListarViajes(DataBase.RutaDb);
                if (lista.Count != 0)
                {
                    Intent intent = new Intent(this, typeof(ListaViajesActivity));
                    StartActivity(intent);
                }
                else
                {
                    Intent intent = new Intent(this, typeof(NuevoViajeActivity));
                    StartActivity(intent);

                }
                Toast.MakeText(this, "Bienvenido " + nombreUsuario.Text, ToastLength.Long).Show();
            }

            else 
                {
                    Toast.MakeText(this, "La contraseña o el usuario son incorrectos", ToastLength.Long).Show();
                    
                }
           



        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
       
    }
}