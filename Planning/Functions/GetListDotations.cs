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
    public class GetListDotations
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GetListDotations(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("GetListDotations")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all pructs with qteStock");
            var d = await _dbContext.QteStock.Include(a=>a.produit).ThenInclude(b=>b.ProduitType).Select(c => new { 
                c.QteStock_Qte,
                Produit_TypeId = c.produit.ProduitType.TypeProduit_Libelle, // Select Produit_TypeId
                Produit_Code = c.produit.Produit_Code,

            })
                .ToListAsync();
            return new OkObjectResult(d);

        }
    }
}
