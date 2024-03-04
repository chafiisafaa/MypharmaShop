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
    public class GetListNatureVitrine
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GetListNatureVitrine(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("GetListNatureVitrine")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all nature of  showcase ");
            var pds = await _dbContext.Nature_Vitrine.Select(c => new { c.NatureVitrine_Id, c.NatureVitrine_Libelle }).ToListAsync();
            return new OkObjectResult(pds);

        }
    }
}
