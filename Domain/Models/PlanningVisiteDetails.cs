namespace Domain.Models
{
    public class PlanningVisiteDetails
    {
        public PlanningVisite PlanningVisite { get; set; }
        public Client planningVisiteClient { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
