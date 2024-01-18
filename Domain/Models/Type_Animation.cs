using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Type_Animation
    {
        public Type_Animation()
        {
            Visites = new HashSet<Visite>();
        }

        public int TypeAnimation_Id { get; set; }
        public string? TypeAnimation_Libelle { get; set; }

        public virtual ICollection<Visite> Visites { get; set; }
    }
}
