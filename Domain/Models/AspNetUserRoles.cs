using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public virtual AspNetRoles Role { get; set; } = null!;
        public virtual AspNetUsers User { get; set; } = null!;
    }
}
