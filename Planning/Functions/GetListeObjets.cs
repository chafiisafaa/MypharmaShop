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
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Planning.Functions
{
    public class GetListeObjets
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GetListeObjets(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("GetListeObjets")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all Objets");
            var pvs = await _dbContext.Objet_Visite.Select(c => new { c.ObjetVisite_Id, c.ObjetVisite_Libelle }).ToListAsync();
            return new OkObjectResult(pvs);
        }
    }
}
