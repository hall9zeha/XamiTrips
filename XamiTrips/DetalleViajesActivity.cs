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
    [Activity(Label = "DetalleViajesActivity")]
    public class DetalleViajesActivity : Activity
    {
        Toolbar detallesToolbar;
        TextView fechaTextView, ciudadTextView;
        ListView detallesListView;
        List<LugarDeInteres> lugaresInteres;
        string ciudadSeleccionada, fechaIda, fechaRegreso;
        int ciudadId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetallesViaje);
            detallesToolbar = FindViewById<Toolbar>(Resource.Id.detallesToolbar);
            fechaTextView = FindViewById<TextView>(Resource.Id.fechaTextView);
            ciudadTextView = FindViewById<TextView>(Resource.Id.ciudadTextView);
            detallesListView = FindViewById<ListView>(Resource.Id.detallesListView);

            //cargamos los datos que en viamos con el bundle a través del intent, en nuestras variables locales
            //si al enviar usamos PutExtras, para extraer los datos enviados usamos solo Extras
            ciudadSeleccionada = Intent.Extras.GetString("Ciudad_seleccionada");
            fechaIda = Intent.Extras.GetString("Fecha_ida");
            fechaRegreso = Intent.Extras.GetString("Fecha_regreso");
            ciudadId = Intent.Extras.GetInt("Ciudad_id");

            ciudadTextView.Text = ciudadSeleccionada;
            fechaTextView.Text = $"{fechaIda}- {fechaRegreso}";
            detallesToolbar.Title = "Mis Visitas";
            SetActionBar(detallesToolbar);
            
            //creamos los pasos para cargar los lugares de interés ingresados en la tabla

            lugaresInteres = DataBase.ListarLugarDeInteres(ciudadId);
            ArrayAdapter arrayAdapterLugares = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,lugaresInteres);
            detallesListView.Adapter = arrayAdapterLugares;

            //creando un evento click de ciudatTextView
            ciudadTextView.Click += CiudadTextView_Click;
        }

        private void CiudadTextView_Click(object sender, EventArgs e)
        {
            double[] latitudes = new double[lugaresInteres.Count], longitudes = new double[lugaresInteres.Count];
            int i = 0;
            foreach (var lugar in lugaresInteres)
            {
                latitudes[i] = lugar.Lat;
                longitudes[i] = lugar.Lng;
                i++;
            }

            //creamos el bundle para mandar los arreglos de latitudes y longitudes al activity de mapa
            Intent intentMapa = new Intent(this, typeof(MapaActivity));
            Bundle bundleMapa = new Bundle();
            bundleMapa.PutDoubleArray("latitudes", latitudes);
            bundleMapa.PutDoubleArray("longitudes", longitudes);

            intentMapa.PutExtras(bundleMapa);
            StartActivity(intentMapa);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Agregar, menu);

            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Agregar")
            {
                

               
                var intent = new Intent(this, typeof(AvenidasActivity));
                Bundle bundle = new Bundle();
                bundle.PutInt("Ciudad_id", ciudadId);
                bundle.PutString("Ciudad_seleccionada", ciudadSeleccionada);
                bundle.PutString("Fecha_regreso", fechaRegreso);
                bundle.PutString("Fecha_ida", fechaIda);
                intent.PutExtras(bundle);
                StartActivity(intent);
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnRestart()
        {

            base.OnRestart();
            lugaresInteres = DataBase.ListarLugarDeInteres(ciudadId);
            ArrayAdapter arrayAdapterLugares = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, lugaresInteres);
            detallesListView.Adapter = arrayAdapterLugares;
        }
    }
}