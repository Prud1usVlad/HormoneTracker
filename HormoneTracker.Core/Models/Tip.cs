using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Tip
    {
        public int TipId { get; set; }
        public int? PatientId { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
