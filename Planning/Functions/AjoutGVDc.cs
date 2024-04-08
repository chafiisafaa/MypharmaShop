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
using Planning.Models;

namespace Planning.Functions
{
    public class AjoutGVDc
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public AjoutGVDc(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("AjoutGVDc")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                RspOfVisiteModel Body = JsonConvert.DeserializeObject<RspOfVisiteModel>(requestBody);

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var visiteModel = new Visite
                        {
                            Visite_PlanningId = Body.Visite.Visite_PlanningId,
                            Visite_MontantVentes = Body.Visite.Visite_MontantVentes,
                            Visite_Commentaire = Body.Visite.Visite_Commentaire,
                            Visite_NombreVitrine = Body.Visite.Visite_NombreVitrine,
                        };

                        var resVisite = await _dbContext.Visite.AddAsync(visiteModel);
                        await _unitOfWork.Complete();

                        if (resVisite.IsKeySet)
                        {
                            var sellOut = new SellOut
                            {
                                SellOut_VisiteId = resVisite.Entity.Visite_Id,
                                SellOut_Animation = Body.SellOutModel.SellOut_Animation,
                                SellOut_NombreAnimationMois = Body.SellOutModel.SellOut_NombreAnimationMois,
                                SellOut_NombreAnimationJour = Body.SellOutModel.SellOut_NombreAnimationJour,
                                SellOut_EvenementTypeId = Body.SellOutModel.SellOut_EvenementTypeId
                            };

                            await _dbContext.SellOut.AddAsync(sellOut);
                            foreach (var v in Body.VisibiliteModel)
                            {
                                var visibilite = new Visibilite
                                {
                                    Visibilite_VisiteId = resVisite.Entity.Visite_Id,
                                    Visibilite_DimenssionHauteur = v.Visibilite_DimenssionHauteur,
                                    Visibilite_DimenssionLargeur = v.Visibilite_DimenssionLargeur,
                                    Visibilite_NatureIdVitrine = v.Visibilite_NatureIdVitrine,
                                    Visibilite_Presence = v.Visibilite_Presence,

                                };

                                var resVisibilite = await _dbContext.Visibilite.AddAsync(visibilite);
                                await _unitOfWork.Complete();
                                foreach (var i in v.Visibilite_urls)
                                {
                                    var visibilitephoto = new Visibilite_Photo
                                    {
                                        VisibilitePhoto_VisibiliteId = resVisibilite.Entity.Visibilite_Id,
                                        VisibilitePhoto_LienPhoto = i,
                                    };

                                    var resVisibilitephoto = await _dbContext.Visibilite_Photo.AddAsync(visibilitephoto);
                                    await _unitOfWork.Complete();

                                }

                            }
                            foreach (var nv in Body.NouveautModel)
                            {
                                var nouveaute = new Nouveaute
                                {
                                    Nouveaute_VisiteId = resVisite.Entity.Visite_Id,
                                    Nouveaute_MarqueId = nv.Nouveaute_MarqueId,
                                    Nouveaute_produitId = nv.Nouveaute_produitId,
                                    Nouveaute_photo = nv.Nouveaute_photo

                                };

                                await _dbContext.Nouveaute.AddAsync(nouveaute);

                            }

                            foreach (var i in Body.Visite.objetsIds)
                            {
                                var vo = new VO
                                {
                                    VO_VisiteId = resVisite.Entity.Visite_Id,
                                    VO_ObjetVisiteId = i,
                                };
                                await _dbContext.VO.AddAsync(vo);
                            }

                            foreach (var stock in Body.EtataStockModel)
                            {
                                var etaStock = new Etat_Stock
                                {
                                    EtatStock_VisiteId = resVisite.Entity.Visite_Id,
                                    EtatStock_StatutId = stock.EtatStock_StatutId,
                                    EtatStock_ProduitId = stock.EtatStock_ProduitId,
                                    EtatStock_Commentaire = stock.EtatStock_Commentaire,
                                };
                                await _dbContext.Etat_Stock.AddAsync(etaStock);
                            }
                            foreach (var mer in Body.MerchandisingModel)
                            {
                                var merch = new Merchandising
                                {
                                    Merchandising_VisiteId = resVisite.Entity.Visite_Id,
                                    Merchandising_TypeId = mer.Merchandising_TypeId,
                                    Merchandising_MarqueId = mer.Merchandising_MarqueId,
                                };
                                var resMerch = await _dbContext.Merchandising.AddAsync(merch);
                                await _unitOfWork.Complete();

                                if (resMerch.IsKeySet)
                                {
                                    foreach (var merphoto in mer.Merchandising_Photo)
                                    {
                                        var merchPhoto = new Merchandising_Photo
                                        {
                                            MerchandisingPhoto_MerchandisingId = resMerch.Entity.Merchandising_Id,
                                            MerchandisingPhoto_LienFichier = merphoto,

                                        };
                                        await _dbContext.Merchandising_Photo.AddAsync(merchPhoto);

                                    }
                                }
                            }
                            foreach (var veille in Body.VeilleConcurrentielleModel)
                            {
                                var veilleC = new Veille_Concurrentielle
                                {
                                    VeilleConcurrentielle_VisiteId = resVisite.Entity.Visite_Id,
                                    VeilleConcurrentielle_TypeId = veille.VeilleConcurrentielle_TypeId,
                                    VeilleConcurrentielle_Commentaire = veille.VeilleConcurrentielle_Commentaire,
                                    VeilleConcurrentielle_LienPhoto = veille.VeilleConcurrentielle_LienPhoto
                                };
                                await _dbContext.Veille_Concurrentielle.AddAsync(veilleC);
                            }
                            foreach (var dd in Body.DotationDistribuesModel)
                            {
                                var dtd = new DotationDistribues
                                {
                                    DotationDistribues_VisiteId = resVisite.Entity.Visite_Id,
                                    DotationDistribues_ProduitId = dd.DotationDistribues_ProduitId,
                                    DotationDistribues_Quantites = dd.DotationDistribues_Quantites,
                                    DotationDistribues_Chanllenge = dd.DotationDistribues_Chanllenge,
                                    DotationDistribues_Total = dd.DotationDistribues_Total
                                };
                                await _dbContext.DotationDistribues.AddAsync(dtd);
                            }
                            foreach (var predVt in Body.PrdVentesParMarqueModel)
                            {
                                var prdVt = new PrdVentesParMarque
                                {
                                    PrdVentesParMarque_VisiteId = resVisite.Entity.Visite_Id,
                                    PrdVentesParMarque_ProduitId = predVt.PrdVentesParMarque_ProduitId,
                                    PrdVentesParMarque_Nombre = predVt.PrdVentesParMarque_Nombre,
                                    PrdVentesParMarque_MarqueId = predVt.PrdVentesParMarque_MarqueId,
                                   
                                };
                                await _dbContext.PrdVentesParMarque.AddAsync(prdVt);
                            }

                            // Add other entities similarly

                            await _unitOfWork.Complete();
                            transaction.Commit();
                            return new OkObjectResult("Visit created successfully");
                        }
                        else
                        {
                            transaction.Rollback();
                            return new BadRequestObjectResult("Couldn't create the visit");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new BadRequestObjectResult(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}
