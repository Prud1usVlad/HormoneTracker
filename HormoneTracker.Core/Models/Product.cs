using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductData = new HashSet<ProductDatum>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Discription { get; set; }

        public virtual ICollection<ProductDatum> ProductData { get; set; }
    }
}
