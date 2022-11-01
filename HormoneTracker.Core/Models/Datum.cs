using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Datum
    {
        public Datum()
        {
            Analyses = new HashSet<Analysis>();
        }

        public int DataId { get; set; }
        public string? Name { get; set; }
        public double? Value { get; set; }
        public double? NormCoefficient { get; set; }

        public virtual ICollection<Analysis> Analyses { get; set; }
    }
}
