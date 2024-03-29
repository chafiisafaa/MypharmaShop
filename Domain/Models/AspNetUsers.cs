﻿using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            Collaborateurs = new HashSet<Collaborateur>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<Collaborateur> Collaborateurs { get; set; }
        public virtual ICollection<PlanningVisite> PlanningVisites { get; set; }
    }
}
