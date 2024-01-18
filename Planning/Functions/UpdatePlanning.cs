using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using Domain.Models;
using Repository.Data;

namespace Planning.Functions
{
    public class UpdatePlanning
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePlanning(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        [FunctionName("UpdatePlanning")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdatePlanning/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updated = JsonConvert.DeserializeObject<PlanningVisite>(requestBody);
                var before = await _dbContext.PlanningVisite.FindAsync(int.Parse(id)); //on a fait le Parse(), car on a declaré "id" comme string 
                if (before == null)
                {
                    log.LogWarning($"Item {id} not found");
                    return new NotFoundResult();
                }
                before.PlanningVisite_ClientId = updated.PlanningVisite_ClientId;
                before.PlanningVisite_DateVisite = updated.PlanningVisite_DateVisite;
                before.PlanningVisite_DateFin = updated.PlanningVisite_DateFin;
                before.PlanningVisite_Color = updated.PlanningVisite_Color;


                await _unitOfWork.Complete();
                return new OkObjectResult(before);

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
