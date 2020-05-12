using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XamiTrips.Clases.Properties;
using XamiTrips.Clases;

namespace XamiTrips
{
    [Activity(Label = "NuevoViajeActivity")]
    public class NuevoViajeActivity : Activity
    {
       
        EditText nombreViaje;
        DatePicker idaDatePicker, regresoDatePicker;
        Button guardarButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NuevoViaje);
            nombreViaje = FindViewById<EditText>(Resource.Id.lugarEditText);
            idaDatePicker = FindViewById<DatePicker>(Resource.Id.idaDatePicker);
            regresoDatePicker = FindViewById<DatePicker>(Resource.Id.regresoDatePicker);
            guardarButton = FindViewById<Button>(Resource.Id.guardarButton);

            guardarButton.Click += GuardarButton_Click;

        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
           

            var nuevoViaje = new Viaje()
            {
                Nombre = nombreViaje.Text,
                FechaInicio = idaDatePicker.DateTime,
                FechaRegreso=regresoDatePicker.DateTime
            };

            if (DataBase.Insertar(ref nuevoViaje, DataBase.RutaDb))
            { Toast.MakeText(this, "Registrado correctamente", ToastLength.Short).Show(); }
            else
            { Toast.MakeText(this, "Error tienes que revisar", ToastLength.Short).Show(); }

            Intent intent = new Intent(this,typeof(ListaViajesActivity));
            StartActivity(intent);

        }
    }
}