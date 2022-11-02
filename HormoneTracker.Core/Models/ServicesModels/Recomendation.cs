using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HormoneTracker.Core.Models.ServicesModels
{
    public class Recomendation
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public Datum Datum { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
