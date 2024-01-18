using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public string UserId { get; set; } = null!;
    }
}
