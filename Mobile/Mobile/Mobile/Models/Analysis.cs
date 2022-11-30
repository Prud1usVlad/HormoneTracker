using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Analysis
    {
        public Analysis()
        {
            Data = new HashSet<Datum>();
        }

        public int AnalysisId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Datum> Data { get; set; }
    }
}
