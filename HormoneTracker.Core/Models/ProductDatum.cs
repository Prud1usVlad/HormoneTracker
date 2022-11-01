using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class ProductDatum
    {
        public int ProductId { get; set; }
        public int DataId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
