using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Data;
using Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Models;
using Planning.Models;
using System.Collections.Generic;

namespace Planning.Functions
{
    public class GetPlanning
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GetPlanning(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("GetPlanning")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all PV");
            var pvs = await _dbContext.PlanningVisite.Include(a => a.planningVisiteClient).Where(a => a.PlanningVisite_Actif == true).ToListAsync();
            var modalPvs = new List<ModalPv>();
            foreach (var planningVisite in pvs)
            {
                var modalPv = new ModalPv
                {
                    PlanningVisite_Id = planningVisite.PlanningVisite_Id,
                    PlanningVisite_CollaborateurId = planningVisite.PlanningVisite_CollaborateurId,
                    PlanningVisite_DateVisite = planningVisite.PlanningVisite_DateVisite,
                    PlanningVisite_HeureVisite = planningVisite.PlanningVisite_HeureVisite,
                    PlanningVisite_ClientId = planningVisite.PlanningVisite_ClientId,
                    PlanningVisite_Realisation = planningVisite.PlanningVisite_Realisation,
                    PlanningVisite_DateFin = planningVisite.PlanningVisite_DateFin,
                    PlanningVisite_Color = planningVisite.PlanningVisite_Color,
                    PlanningVisite_Actif = planningVisite.PlanningVisite_Actif,
                    PlanningVisite_userId = planningVisite.PlanningVisite_userId,
                    planningVisiteClient = new ClientView()
                    {
                        Client_Id = planningVisite.planningVisiteClient.Client_Id,
                        Client_RaisonSociale = planningVisite.planningVisiteClient.Client_RaisonSociale,
                    }
                };
                modalPvs.Add(modalPv);
            }
            return new OkObjectResult(modalPvs);

        }
    }
}
