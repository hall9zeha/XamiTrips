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

namespace XamiTrips.Clases
{
    public class Avenidas
    {
        public VenuesResponse Response { get; set; }
    }
    public class VenuesResponse
    {
        public List<Venue> Venues { get; set; }
    }
    public class Venue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location location { get; set; }
        public List<Category> Categories { get; set; }

        public override string ToString()
        {
            if (Categories.Count > 0)
            {
                return $"{Name}\t ({Categories[0].Name})";

            }
            else { return Name; }
        }
        public class Location
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }
    }
}