using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int? AmountLast { get; set; }
        public string Period { get; set; }
        public DateTime? LastDoseDate { get; set; }
        public int? PatientId { get; set; }
    }
}
