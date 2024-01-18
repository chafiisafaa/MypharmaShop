using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Web.Models;

namespace Repository.IRepositories
{
    public interface IPlanningVisiteRepository
    {
        Task<List<Client>> GetListeClients();
        List<PlanningVisite> GetListePlanning(string userId);
        List<PlanningVisiteDetails> GetListePlanning();
        Task AddPlanningViste(PlanningVisite planningVisite);
        Task UpdatePlanningViste(PlanningVisite planningVisite);
        Task DeletePlanningViste(int PlanningVisiteId);
        Task<List<AspNetUserRoles>> GetListUsers(string roleId);

    }
}
