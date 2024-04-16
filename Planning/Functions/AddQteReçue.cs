using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Domain.Models;
using Repository.Data;
using Repository.UnitOfWork;

namespace Planning.Functions
{
    public class AddQteReçue
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public AddQteReçue(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        [FunctionName("AddQteReçue")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddQteReçue/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updated = JsonConvert.DeserializeObject<Affectations>(requestBody);
                var before = await _dbContext.Affectations.FindAsync(int.Parse(id)); //on a fait le Parse(), car on a declaré "id" comme string 
                if (before == null)
                {
                    log.LogWarning($"Item {id} not found");
                    return new NotFoundResult();
                }
                before.Affectations_QteReçue = updated.Affectations_QteReçue;
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
