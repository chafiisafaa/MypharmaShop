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
using System.Linq;

namespace Planning.Functions
{
    public class SuividesAffectations
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public SuividesAffectations(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("SuividesAffectations")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all pructs with qteStock");
            var d = await _dbContext.Affectations.Include(a=>a.user).Include(a => a.produit).ThenInclude(b => b.ProduitType).Select(c => new {
               
                Produit_TypeId = c.produit.ProduitType.TypeProduit_Libelle, // Select Produit_TypeId
                Produit_Code = c.produit.Produit_Code,
                Produit_designation=c.produit.Produit_Designation,
                user=c.user.UserName,
                Produit_id=c.produit.Produit_Id,
                c.Affectations_QteAffecte,
                c.Affectations_QteReçue


            })
                .ToListAsync();
            return new OkObjectResult(d);
        }
    }
}
