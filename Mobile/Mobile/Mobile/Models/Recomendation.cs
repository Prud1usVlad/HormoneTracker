using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Recomendation
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public Datum Datum { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
