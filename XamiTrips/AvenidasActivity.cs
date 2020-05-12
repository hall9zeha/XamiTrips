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
    [Activity(Label = "AvenidasActivity")]
    public class AvenidasActivity : Activity
    {
        EditText filtroEditText;
        Spinner categoriaSpinner;
        ListView avenidasListView;
        List<Category> categorias= new List<Category>();
        List<Venue> Avenidas=new List<Venue>();
        string ciudadSeleccionada;
        int ciudadId;
        //al usar métodos asincronos o llamadas a dichos métodos la función o método donde lo realizamos tambien tiene que volverse asíncrono
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NuevoLugar);
           

            filtroEditText = FindViewById<EditText>(Resource.Id.filtroEditText);
            categoriaSpinner = FindViewById<Spinner>(Resource.Id.categoriaSpinner);
            avenidasListView = FindViewById<ListView>(Resource.Id.avenidasListView);
            Toolbar nuevoLugarToolbar = FindViewById<Toolbar>(Resource.Id.nuevoLugarToolbar);

            avenidasListView.ChoiceMode = ChoiceMode.Multiple;
            SetActionBar(nuevoLugarToolbar);
            ActionBar.Title = "Nuevo Destino";

            FoursQuare helper = new FoursQuare();
            categorias = await helper.ObtenerCategorias();

            ciudadSeleccionada = Intent.Extras.GetString("Ciudad_seleccionada");
            ciudadId = Intent.Extras.GetInt("Ciudad_id");

            var spinnerAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, categorias);
            categoriaSpinner.Adapter = spinnerAdapter;

            categoriaSpinner.ItemSelected += CategoriaSpinner_ItemSelected;
            filtroEditText.TextChanged += FiltroEditText_TextChanged;


        }



        async void CategoriaSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var categoriaSeleccionada = categorias[e.Position];
            FoursQuare helperNew = new FoursQuare();
            Avenidas = await helperNew.ObtenerAvenidas(ciudadSeleccionada, categoriaSeleccionada.Id);

            var listAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemMultipleChoice, Avenidas);
            avenidasListView.Adapter = listAdapter;
        }
        private void FiltroEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var listaFiltrada = Avenidas.Where(v => v.Name.ToLower().Contains(filtroEditText.Text.ToLower())).ToList();
            var listAdapterFiltro = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemMultipleChoice, listaFiltrada);
            avenidasListView.Adapter = listAdapterFiltro;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Guardar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)

        {
            
            if (item.TitleFormatted.ToString() == "Guardar")
            {
                var posicionesSeleccionadas = avenidasListView.CheckedItemPositions;
                for (int i = 0; i < posicionesSeleccionadas.Size(); i++)
                {
                    var textoCelda = avenidasListView.Adapter.GetItem(posicionesSeleccionadas.KeyAt(i)).ToString();
                    var avenidasSeleccionadas = Avenidas.Where(v => textoCelda.Contains(v.Name)).First();

                    //creando el objeto para guardar los datos de un nuevo lugar de interés

                LugarDeInteres lugarDeInteres = new  LugarDeInteres(avenidasSeleccionadas, ciudadId);
                   if (DataBase.Insertar(ref lugarDeInteres, DataBase.RutaDb))
                      Toast.MakeText(this, "Nuevo Sitio de interes Agregado", ToastLength.Long).Show();
                      
                }

                //cargando un bundle para retornar datos a la página de detalles de viaje una vez seleccionado nuevo 
                //lugar de interes
                string fechaIda = Intent.Extras.GetString("Fecha_ida");
                string fechaRegreso = Intent.Extras.GetString("Fecha_regreso");
                Bundle bundleCiudad = new Bundle();
                bundleCiudad.PutString("Ciudad_seleccionada",ciudadSeleccionada);
                bundleCiudad.PutInt("Ciudad_id", ciudadId);
                bundleCiudad.PutString("Fecha_ida", fechaIda);
                bundleCiudad.PutString("Fecha_regreso", fechaRegreso);
                Intent intentRegresar = new Intent(this, typeof(DetalleViajesActivity));
                intentRegresar.PutExtras(bundleCiudad);

                StartActivity(intentRegresar);
                
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}