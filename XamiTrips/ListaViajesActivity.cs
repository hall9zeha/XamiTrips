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
using XamiTrips.Clases.Properties;
using XamiTrips.Clases;
using System.IO;

namespace XamiTrips
{
    [Activity(Label = "ListaViajesActivity")]
    public class ListaViajesActivity : Activity
    {
     
        Toolbar listaToolbar;
        ListView listaViajes;

        List<Viaje> viajes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListaViajeslayout);
            listaToolbar = FindViewById<Toolbar>(Resource.Id.viajesToolbar);
            listaViajes = FindViewById<ListView>(Resource.Id.ViajesListView);
            //crearemos un evento para navegar a otro luagr cuando hagamos click en un elemento de la lista
            listaViajes.ItemClick += ListaViajes_ItemClick;
            SetActionBar(listaToolbar);
            ActionBar.Title = "Mis Viajes";
            
            viajes = new List<Viaje>();
            viajes = DataBase.ListarViajes(DataBase.RutaDb);
            var ArrayAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, viajes);
            listaViajes.Adapter = ArrayAdapter;
                
          
            //ListAdapter = ArrayAdapter;
        }

        private void ListaViajes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //tomamos la posicion del elemento seleccionado a travez del evento e, y lo volcamos a la variable viaje seleccionado
            var viajeSeleccionado = viajes[e.Position];
            var intent = new Intent(this, typeof(DetalleViajesActivity));

            //ahora para enviar parametros de la ciudad seleccionada y filtrar en detallesViajeActivity lo hacemos con bundle
            var bundle = new Bundle();
            bundle.PutString("Ciudad_seleccionada", viajeSeleccionado.Nombre);
            bundle.PutInt("Ciudad_id", viajeSeleccionado.Id);
            bundle.PutString("Fecha_ida", viajeSeleccionado.FechaInicio.ToString("MMM dd"));
            bundle.PutString("Fecha_regreso", viajeSeleccionado.FechaRegreso.ToString("MMM dd"));
            //Pasamos los parámetros al intent que los llevará a la nueva activity, con el método putExtras, y le agregamos el bundle
            intent.PutExtras(bundle);
            StartActivity(intent);
        }

        protected override void OnRestart()
        {
            base.OnRestart();

            viajes = new List<Viaje>();
            viajes = DataBase.ListarViajes(DataBase.RutaDb);

            var ArrayAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, viajes);
            listaViajes.Adapter = ArrayAdapter;
            // ListAdapter = ArrayAdapter;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Agregar,menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Agregar")
            {
                Intent intent = new Intent(this,typeof(NuevoViajeActivity));
                StartActivity(intent);
            }
            return base.OnOptionsItemSelected(item);
        }
    }

}