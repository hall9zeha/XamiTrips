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
   public  class Categorias
    {
        public Response Response { get; set; }
    }
    public class Response
    {
        public List<Category> Categories { get; set; }
    }
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public override string ToString()
        {
            
                return Name;
        }

    }
    
}