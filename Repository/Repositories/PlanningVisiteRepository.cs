using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Web.Models;

namespace Repository.Repositories
{
    public class PlanningVisiteRepository : IPlanningVisiteRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PlanningVisiteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPlanningViste(PlanningVisite planningVisite)
        {
            await _dbContext.PlanningVisite.AddAsync(planningVisite);
        }

        public Task DeletePlanningViste(int PlanningVisiteId)
        {
            var PV = _dbContext.PlanningVisite.FirstOrDefault(x => x.PlanningVisite_Id == PlanningVisiteId);
            if (PV != null)
            {
                PV.PlanningVisite_Actif = false;

            }
            return _dbContext.SaveChangesAsync();
        }

        public async Task<List<Client>> GetListeClients()
        {
            var res = await _dbContext.Client.ToListAsync();
            return res;
        }

        public List<PlanningVisite> GetListePlanning(string userId)
        {
            var res = _dbContext.PlanningVisite.Include(a=>a.planningVisiteClient).Where(a=>a.PlanningVisite_Actif==true && a.PlanningVisite_userId== userId).ToList();
            return res;
        }

        public List<PlanningVisiteDetails> GetListePlanning()
        {
            var result = _dbContext.PlanningVisite
                .Include(pv => pv.planningVisiteClient)
                .Include(pv => pv.user)
                .Where(pv => pv.PlanningVisite_Actif == true)
                .Join(
                    _dbContext.AspNetUserRoles,
                    pv => pv.PlanningVisite_userId,
                    ur => ur.UserId,
                    (pv, ur) => new { PlanningVisite = pv, UserRole = ur }
                )
                .Join(
                    _dbContext.AspNetRoles,
                    ur => ur.UserRole.RoleId,
                    role => role.Id,
                    (ur, role) => new { ur.PlanningVisite, ur.UserRole, Role = role }
                )
                .Select(joined => new PlanningVisiteDetails
                {
                    PlanningVisite = joined.PlanningVisite,
                    planningVisiteClient = joined.PlanningVisite.planningVisiteClient,
                    UserName = joined.PlanningVisite.user.UserName,
                    RoleName = joined.Role.Name
                })
                .ToList();

            return result;
        }


        public Task<List<AspNetUserRoles>> GetListUsers(string roleId)
        {
            var res = _dbContext.AspNetUserRoles.Include(a => a.User).Where(a => a.RoleId == roleId).ToListAsync();
            return res;
        }

        public Task UpdatePlanningViste(PlanningVisite planningVisite)
        {
            var planningVisitebefore = _dbContext.PlanningVisite.FirstOrDefault(x => x.PlanningVisite_Id == planningVisite.PlanningVisite_Id);
            if (planningVisite.PlanningVisite_ClientId != null)
            {
                planningVisitebefore.PlanningVisite_ClientId = planningVisite.PlanningVisite_ClientId;


            }
            if (planningVisite.PlanningVisite_DateVisite != null)
            {
                planningVisitebefore.PlanningVisite_DateVisite = planningVisite.PlanningVisite_DateVisite;


            }
            if (planningVisite.PlanningVisite_DateFin != null)
            {
                planningVisitebefore.PlanningVisite_DateFin = planningVisite.PlanningVisite_DateFin;


            }
            if (planningVisite.PlanningVisite_Color != null)
            {
                planningVisitebefore.PlanningVisite_Color = planningVisite.PlanningVisite_Color;


            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
