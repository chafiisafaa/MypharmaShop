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

    }
}
