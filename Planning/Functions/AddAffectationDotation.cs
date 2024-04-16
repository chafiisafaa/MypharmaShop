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
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.UnitOfWork;

namespace Planning.Functions
{
    public class AddAffectationDotation
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public AddAffectationDotation(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("AddAffectationDotation")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                Affectations affect = JsonConvert.DeserializeObject<Affectations>(requestBody);
                // Add the new PV to the database
                var res = await _dbContext.Affectations.AddAsync(affect);
                await _unitOfWork.Complete();

                // Check if the PV was added successfully
                if (res.IsKeySet)
                    return new OkObjectResult("affect created successfully");
                return new BadRequestObjectResult("Couldn't create the affect");
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.Message);

            }
        }
    }
}
