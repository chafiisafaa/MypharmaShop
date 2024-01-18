﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Web.Models;

namespace Service.IServices
{
    public interface IPlanningVisiteService
    {
        Task<List<Client>> GetListeClients();
        List<PlanningVisite> GetListePlanning(string userId);
        List<PlanningVisiteDetails> GetListePlanning();
        Task<bool> AddPlanningViste(PlanningVisite planningVisite);
        Task<bool> UpdatePlanningViste(PlanningVisite planningVisite);
        Task<bool> DeletePlanningViste(int PlanningVisiteId);
        Task<List<AspNetUserRoles>> GetListUsers(string roleId);

    }
}
