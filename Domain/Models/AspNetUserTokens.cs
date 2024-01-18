﻿using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class AspNetUserTokens
    {
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
