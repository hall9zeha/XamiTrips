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
using XamiTrips.Clases.Properties;

namespace XamiTrips.Clases
{
    public class InicioSesion
    {
        static SQLiteConnection cn = new SQLiteConnection(DataBase.RutaDb);
        public static bool ValidarSesion(string usuario, string password)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                return false;
            
            else
                return true;
        }
        public static bool ValidarSesionDb(string usuario, string password)
        {
            List<Usuario> listaUser = new List<Usuario>();
            listaUser = cn.Table<Usuario>().Where(u => u.nombreUsuario == usuario && u.Contraseña == password).ToList();
            if (listaUser.Count > 0)
                return true;
            else
                return false;
        }
      
    }
}