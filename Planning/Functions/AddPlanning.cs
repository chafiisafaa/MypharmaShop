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
using Repository.Data;
using Repository.UnitOfWork;
using Domain.Models;

namespace Planning.Functions
{
    public class AddPlanning
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public AddPlanning(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("AddPlanning")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                PlanningVisite PVBody = JsonConvert.DeserializeObject<PlanningVisite>(requestBody);
                // Add the new PV to the database
                var res = await _dbContext.PlanningVisite.AddAsync(PVBody);
                await _unitOfWork.Complete();

                // Check if the PV was added successfully
                if (res.IsKeySet)
                    return new OkObjectResult("Message created successfully");
                return new BadRequestObjectResult("Couldn't create the Message");
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.Message);

            }
        }
    }
}
