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
    public class AddTypeEvenement
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public AddTypeEvenement(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("AddTypeEvenement")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                Evenement_Type EvenetType = JsonConvert.DeserializeObject<Evenement_Type>(requestBody);
                // Add the new PV to the database
                var res = await _dbContext.Evenement_Type.AddAsync(EvenetType);
                await _unitOfWork.Complete();

                // Check if the PV was added successfully
                if (res.IsKeySet)
                    return new OkObjectResult("Event Type created successfully");
                return new BadRequestObjectResult("Couldn't create the Event Type");
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.Message);

            }

        }
    }
}
