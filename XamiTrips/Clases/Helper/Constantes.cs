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

namespace XamiTrips.Clases.Helper
{
    public class Constantes
    {
        public const string CLIENT_ID = "TA4G34WULVBY2UDOJ0QKBAQPAZEKLWM0GPYKXUQ3SC4VFRMY";
        public const string CLIENT_SECRET = "WZBDRDB2QK5E4CHHM5YEXGMLK3QMFUN3Y11B2K4ZS1F1JVP4";
        public const string pruebaidcat = "4bf58dd8d48988d1e1931735";
        public static string ObtenerUrlCategorias()
        {
            //return $"https://api.foursquare.com/v2/venues/categories?client_id={CLIENT_ID}&client_secret={CLIENT_SECRET}&v={DateTime.Now.ToString("yyyyMMdd")}";
            return "https://api.foursquare.com/v2/venues/categories?client_id=" + CLIENT_ID + "&client_secret=" + CLIENT_SECRET
                   + "&v=" + DateTime.Now.ToString("yyyyMMdd");
        }
        public static string ObtenerUrlAvenidas(string ciudad, string idCategoria)
        {
            //return $"https://api.foursquare.com/v2/venues/search?near={ciudad}&categoryId={idCategoria}&client_id={CLIENT_ID}&client_secret{CLIENT_SECRET}&v={DateTime.Now.ToString("yyyyMMdd")}";
            return "https://api.foursquare.com/v2/venues/search?near=" + ciudad + "&categoryId=" + idCategoria + "&client_id=" + CLIENT_ID
                + "&client_secret=" + CLIENT_SECRET + "&v=" + DateTime.Now.ToString("yyyyMMdd");

        }
       
    }
}