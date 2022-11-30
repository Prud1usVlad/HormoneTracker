using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Product
    {
        public Product()
        {
            Data = new HashSet<Datum>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        public virtual ICollection<Datum> Data { get; set; }
    }
}
