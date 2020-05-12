using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamiTrips.Clases.Properties
{
    public class Usuario {
        [PrimaryKey , AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string nombreUsuario { get; set; }
        public string Contraseña { get; set; }



    }
   public  class Viaje
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaRegreso { get; set; }

        public override string ToString()
        {
            return $"{Nombre}\t({FechaInicio:d}-{FechaRegreso:d})";
        }
    }
    public class LugarDeInteres
    {
        public LugarDeInteres()
        {

        }
        public LugarDeInteres(Venue venue, int idViaje)
        {
            IdViaje = idViaje;
            Nombre = venue.Name;
            Lat =(float)venue.location.Lat;
            Lng = (float)venue.location.Lng;
            Categoria = venue.Categories.First().Name;

           
    }
        [PrimaryKey,AutoIncrement ]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public int IdViaje { get; set; }
        public string Categoria { get; set; }
        public override string ToString()
        {
            return Nombre;
        }

    }
}