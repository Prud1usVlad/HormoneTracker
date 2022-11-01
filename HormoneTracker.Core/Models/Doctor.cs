using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }

        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MidName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
