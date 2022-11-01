using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Analysis
    {
        public Analysis()
        {
            Data = new HashSet<Datum>();
        }

        public int AnalysisId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public int? StatusId { get; set; }
        public string? Name { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Status? Status { get; set; }

        public virtual ICollection<Datum> Data { get; set; }
    }
}
