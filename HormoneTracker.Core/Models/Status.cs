using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Status
    {
        public Status()
        {
            Analyses = new HashSet<Analysis>();
        }

        public int StatusId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Analysis> Analyses { get; set; }
    }
}
