using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Analyses = new HashSet<Analysis>();
            Medicines = new HashSet<Medicine>();
            Tips = new HashSet<Tip>();
        }

        public int PatientId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MidName { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Analysis> Analyses { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<Tip> Tips { get; set; }
    }
}
