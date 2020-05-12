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
using System.Threading.Tasks;
using XamiTrips.Clases.Helper;
using System.Net.Http;
using Newtonsoft.Json;

namespace XamiTrips.Clases
{
   public  class FoursQuare
    {
        public async Task<List<Category>> ObtenerCategorias()
        {
            List<Category> categorias = new List<Category>();

            var url = Constantes.ObtenerUrlCategorias();

            using (HttpClient cliente = new HttpClient())
            {
                var respuesta = await cliente.GetStringAsync(url);

                Categorias categoriasJson = JsonConvert.DeserializeObject<Categorias>(respuesta);
                categorias = categoriasJson.Response.Categories;
            }
                return categorias;
        }
        public async Task<List<Venue>> ObtenerAvenidas(string ciudad, string idCategoria)
        {
            List<Venue> venues = new List<Venue>();
           var url = Constantes.ObtenerUrlAvenidas(ciudad,idCategoria);
            using (HttpClient cliente = new HttpClient())
            {
                string response = await cliente.GetStringAsync(url);
                Avenidas respuestaJson = JsonConvert.DeserializeObject<Avenidas>(response);
                venues = respuestaJson.Response.Venues;

            }
            return venues;
        }
    }
}