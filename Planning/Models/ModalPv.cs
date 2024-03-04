using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class ModalPv
    {
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
        public ClientView? planningVisiteClient { get; set; }
        //public AspNetUsers? user { get; set; }
    }
}
