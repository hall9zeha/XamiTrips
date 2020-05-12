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
using System.IO;
using XamiTrips.Clases.Properties;
using System.Threading.Tasks;

namespace XamiTrips.Clases
{
    public  class DataBase
    {
        static string nombreDb = "Viajes.db3";
        static string rutaDatabase = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static string rutaDb;

        static SQLiteConnection Cn = new SQLiteConnection(RutaDb);
        public static  string RutaDb
        {
            get {
                
                    rutaDb = Path.Combine(rutaDatabase,nombreDb);
                
                return rutaDb;
            }
        }
        
        public static bool Insertar<T>(ref T item, string ruta)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ruta))
            {
                cn.CreateTable<T>();
                if (cn.Insert(item) > 0)
                { return true; }
            };
            return false;
        }

        public static int Insertar<Usuario>( Usuario objUsuario)
        {
           
                Cn.CreateTable<Usuario>();
               return Cn.Insert(objUsuario);
            

        }
       
        public static void Modificar(Viaje obj, string ruta)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ruta))
            {

                cn.Update(obj);
            
            };
        }
        //public static List<Viaje> TraerViajes(string ruta)
        //{
        //    List<Viaje> viajes = new List<Viaje>();

        //    using (var cn = new SQLiteConnection(ruta))
        //    {
        //        viajes = cn.Table<Viaje>().ToList();
        //    };
        //    return viajes;
        //}
        public static List<Viaje> ListarViajes(string rut)
        {
            List<Viaje> v = new List<Viaje>();

            using (var cn = new SQLiteConnection(rut))
            {
                cn.CreateTable<Viaje>();
                v = cn.Table<Viaje>().ToList();
            };
          return  v;
        }
        //creamos el método que nos devolverá los lugares de interes
        public static List<LugarDeInteres> ListarLugarDeInteres(int idViaje)
        {
            List<LugarDeInteres> lstLugarDeInteres = new List<LugarDeInteres>();

            using (SQLiteConnection cn = new SQLiteConnection(RutaDb))
            {
                cn.CreateTable<LugarDeInteres>();
                lstLugarDeInteres = cn.Table<LugarDeInteres>().Where(l => l.IdViaje == idViaje).ToList();


            };
            return lstLugarDeInteres;

        }
    }
}