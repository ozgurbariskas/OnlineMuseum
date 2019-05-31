using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.WebMVC.Models
{
    public class Exhibition
    {
        public int id { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public string image { get; set; }
        
    }
}
