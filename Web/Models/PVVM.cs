using Domain.Models;

namespace Web.Models
{
    public class PVVM
    {
        public PVVM()
        {
            pvs = new List<PlanningVisiteDetails>();
        }
        public PlanningVisite? pvUpdate { get; set; }
        public List<PlanningVisiteDetails>? pvs { get; set; }
        public PlanningVisite? pv { get; set; }
        public string? nvId { get; set; }
        public string? userId { get; set; } 
        public int? clientId { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
        public bool? etat { get; set; }


    }
}
