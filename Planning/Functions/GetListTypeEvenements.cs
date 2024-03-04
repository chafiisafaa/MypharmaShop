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

namespace Planning.Functions
{
    public class GetListTypeEvenements
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GetListTypeEvenements(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("GetListTypeEvenements")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all Brand ");
            var evenement_Types = await _dbContext.Evenement_Type.Select(c => new { c.EvenementType_Id, c.EvenementType_Libelle }).ToListAsync();
            return new OkObjectResult(evenement_Types);

        }
    }
}
