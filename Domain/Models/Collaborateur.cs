using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Collaborateur
    {
        public Collaborateur()
        {
            PlanningVisites = new HashSet<PlanningVisite>();
        }

        public int Collaborateur_Id { get; set; }
        public string? Collaborateur_Nom { get; set; }
        public string? Collaborateur_Prenom { get; set; }
        public string? Collaborateur_UserId { get; set; }
        public int? Collaborateur_VilleId { get; set; }
        public bool? Collaborateur_Active { get; set; }

        public virtual AspNetUsers? CollaborateurUser { get; set; }
        public virtual Ville? CollaborateurVille { get; set; }
        public virtual ICollection<PlanningVisite> PlanningVisites { get; set; }
    }
}
