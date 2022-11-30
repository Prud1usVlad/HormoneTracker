using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Tip
    {
        public int TipId { get; set; }
        public int? PatientId { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }

    }
}
