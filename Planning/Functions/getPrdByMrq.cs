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
    public class getPrdByMrq
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public getPrdByMrq(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("FilterMqId")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "FilterMqId/{mrqId}")] HttpRequest req,
            ILogger log,int mrqId)
        {

            var Prdts = await _dbContext.Produit.Select(c => new { c.Produit_Id, c.Produit_Designation,c.Produit_MarqueId }).Where(c=>c.Produit_MarqueId== mrqId).ToListAsync();
            return new OkObjectResult(Prdts);
        }
    }
}
