using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamiTrips.Clases;
using XamiTrips.Clases.Properties;

namespace XamiTrips
{
    [Activity(Label = "CrearCuentaActivity")]
    public class CrearCuentaActivity : Activity
    {
        Toolbar accountToolbar;
        EditText emailText, nombreText, usuarioText, contraseñaText, confirmarText;
        Button registroButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CrearCuenta);

            accountToolbar = FindViewById<Toolbar>(Resource.Id.cuentaToolbar);
            emailText = FindViewById<EditText>(Resource.Id.emailEditText);
            nombreText = FindViewById<EditText>(Resource.Id.nombreEditText);
            usuarioText = FindViewById<EditText>(Resource.Id.usuarioEditText);
            contraseñaText = FindViewById<EditText>(Resource.Id.ContraseñaEditText);
            confirmarText = FindViewById<EditText>(Resource.Id.confirmarEditText);
            registroButton = FindViewById<Button>(Resource.Id.registroButton);

            registroButton.Click += RegistroButton_Click;
            SetActionBar(accountToolbar);
            ActionBar.Title="Crear Cuenta";
            //Activamos el boton de UpMenu del toolbar
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
           

        }
        //Método sobreescrito para volver al activity anterior usando el boton UpMenu del toolbar 
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
       
        private void RegistroButton_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario()
            {
                Email = emailText.Text,
                Nombre=nombreText.Text,
                nombreUsuario=usuarioText.Text,
                Contraseña=contraseñaText.Text

            };
            int i= DataBase.Insertar(usuario);
            if (i >0)
                Toast.MakeText(this, "Te has Registrado correctamente", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Error en la transacción", ToastLength.Long).Show();

            Intent intentReturn = new Intent(this, typeof(MainActivity));
            StartActivity(intentReturn);
        }
    }
}