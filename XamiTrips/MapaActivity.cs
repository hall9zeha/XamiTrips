using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamiTrips
{
    [Activity(Label = "MapaActivity")]
    public class MapaActivity : Activity, IOnMapReadyCallback
    {
        double[] latitudes, longitudes;
        public void OnMapReady(GoogleMap googleMap)
        {
            for (int i = 0; i < latitudes.Length; i++)
            {
                MarkerOptions options = new MarkerOptions();
                options.SetPosition(new LatLng(latitudes[i], longitudes[i]));

                googleMap.AddMarker(options);
            }


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Mapa);

            latitudes = Intent.Extras.GetDoubleArray("latitudes");
            longitudes = Intent.Extras.GetDoubleArray("longitudes");

            MapFragment fragmento = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapa);
            fragmento.GetMapAsync(this);
        }
    }
}