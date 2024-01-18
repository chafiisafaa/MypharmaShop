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
using Domain.Models;

namespace Planning.Functions
{
    public class DeletePlanning
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public DeletePlanning(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("DeletePlanning")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "DeletePlanning/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            try
            {
                var before = await _dbContext.PlanningVisite.FindAsync(int.Parse(id)); //on a fait le Parse(), car on a declaré "id" comme string 
                if (before == null)
                {
                    log.LogWarning($"Item {id} not found");
                    return new NotFoundResult();
                }
                before.PlanningVisite_Actif = false;

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
