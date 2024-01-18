using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using System.Diagnostics;
using Web.Models;

namespace Service.Services
{
    public class PlanningVisiteService : IPlanningVisiteService
    {
        private readonly IPlanningVisiteRepository planningVisiteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PlanningVisiteService(IPlanningVisiteRepository planningVisiteRepository, IUnitOfWork unitOfWork)
        {
            this.planningVisiteRepository = planningVisiteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddPlanningViste(PlanningVisite planningVisite)
        {
            try
            {
                await planningVisiteRepository.AddPlanningViste(planningVisite);
                await _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<bool> DeletePlanningViste(int PlanningVisiteId)
        {
            try
            {
                await planningVisiteRepository.DeletePlanningViste(PlanningVisiteId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Client>> GetListeClients()
        {
            var res = await planningVisiteRepository.GetListeClients();
            return res;
        }

        public List<PlanningVisite> GetListePlanning(string userId)
        {
            var res = planningVisiteRepository.GetListePlanning(userId);
            return res;
        }

        public List<PlanningVisiteDetails> GetListePlanning()
        {
            var res = planningVisiteRepository.GetListePlanning();
            return res;
        }

        public async Task<List<AspNetUserRoles>> GetListUsers(string roleId)
        {
            var res = await planningVisiteRepository.GetListUsers(roleId);
            return res;
        }

        public async Task<bool> UpdatePlanningViste(PlanningVisite planningVisite)
        {
            try
            {
                await planningVisiteRepository.UpdatePlanningViste(planningVisite);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
