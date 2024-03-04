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

        public List<PlanningVisiteDetails> GetListePlanning(string? nvId, string? userId, int? clientId, DateTime? dateDebut, DateTime? dateFin, bool? etat)
        {
            var result = _dbContext.PlanningVisite
                            .Include(pv => pv.planningVisiteClient)
                            .Include(pv => pv.user)
                            .Where(pv => pv.PlanningVisite_Actif == true);

            // Apply filters based on parameters
            if (userId != null)
            {
                result = result.Where(pv => pv.PlanningVisite_userId == userId);
            }

            if (clientId != null)
            {
                result = result.Where(pv => pv.PlanningVisite_ClientId == clientId.Value);
            }

            if (dateDebut != null)
            {
                result = result.Where(pv => pv.PlanningVisite_DateVisite >= dateDebut.Value);
            }

            if (dateFin != null)
            {
                result = result.Where(pv => pv.PlanningVisite_DateFin <= dateFin.Value);
            }

            if (etat != null)
            {
                result = result.Where(pv => pv.PlanningVisite_Realisation == etat.Value);
            }
            if (nvId != null)
            {
                result = result
                    .Join(
                        _dbContext.AspNetUserRoles,
                        pv => pv.PlanningVisite_userId,
                        ur => ur.UserId,
                        (pv, ur) => new { PlanningVisite = pv, UserRole = ur }
                    )
                    .Where(joined => joined.UserRole.RoleId == nvId)
                    .Select(joined => joined.PlanningVisite);
            }
            else
            {
                result = result
                   .Join(
                       _dbContext.AspNetUserRoles,
                       pv => pv.PlanningVisite_userId,
                       ur => ur.UserId,
                       (pv, ur) => new { PlanningVisite = pv, UserRole = ur }
                   )
                   .Select(joined => joined.PlanningVisite);

            }

            // Continue with the rest of your query

            var finalResult = result
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

            return finalResult;
        }

        public List<AspNetRoles> GetListeRoles()
        {
            var res = _dbContext.AspNetRoles.ToList();
            return res;
        }

        public List<AspNetUsers> GetListeUsers()
        {
            var res = _dbContext.AspNetUsers.ToList();
            return res;
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
