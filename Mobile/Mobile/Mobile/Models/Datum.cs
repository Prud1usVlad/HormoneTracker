using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Datum
    {
        public Datum()
        {
            Analyses = new HashSet<Analysis>();
            Products = new HashSet<Product>();
        }

        public int DataId { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
        public double? NormCoefficient { get; set; }

        public virtual ICollection<Analysis> Analyses { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
