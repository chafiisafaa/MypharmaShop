using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Nature_Vitrine
    {
        public Nature_Vitrine()
        {
            Visibilites = new HashSet<Visibilite>();
        }

        public int NatureVitrine_Id { get; set; }
        public string? NatureVitrine_Libelle { get; set; }

        public virtual ICollection<Visibilite> Visibilites { get; set; }
    }
}
