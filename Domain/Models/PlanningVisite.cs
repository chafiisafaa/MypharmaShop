using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public partial class PlanningVisite
    {
        public PlanningVisite()
        {
            
        }

        public int PlanningVisite_Id { get; set; }
        public int? PlanningVisite_CollaborateurId { get; set; }
        public DateTime? PlanningVisite_DateVisite { get; set; }
        public string? PlanningVisite_HeureVisite { get; set; }
        public int? PlanningVisite_ClientId { get; set; }
        public bool? PlanningVisite_Realisation { get; set; }
        public DateTime? PlanningVisite_DateFin { get; set; }
        public string? PlanningVisite_Color { get; set; }
        public bool? PlanningVisite_Actif { get; set; }
        public string? PlanningVisite_userId { get; set; }
        [NotMapped]
        public int? PVId { get; set; }

        public Client? planningVisiteClient { get; set; }
        public Collaborateur? PlanningVisiteCollaborateur { get; set; }
        public ICollection<Visite> Visites { get; set; }
        public AspNetUsers? user { get; set; }
    }
}
