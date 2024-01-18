using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Nouveaute
    {
        public int Nouveaute_Id { get; set; }
        public int? Nouveaute_VisiteId { get; set; }
        public int? Nouveaute_MarqueId { get; set; }

        public virtual Marque? NouveauteMarque { get; set; }
        public virtual Visite? NouveauteVisite { get; set; }
    }
}
