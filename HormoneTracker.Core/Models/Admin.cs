﻿using System;
using System.Collections.Generic;

namespace HormoneTracker.Core.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
