using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;
using Repository.Data;
using Repository.UnitOfWork;
using Service.IServices;
using Domain.Models;
using Web.Models;
using Service.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string[] _scopes;
        private readonly IAuthentificationService _authentificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        private readonly IPlanningVisiteService _planningVisiteService;

        public HomeController(ITokenAcquisition tokenAcquisition, IConfiguration configuration, IAuthentificationService authentificationService, IUnitOfWork unitOfWork, ApplicationDbContext dbContext, IPlanningVisiteService planningVisiteService)
        {
            _planningVisiteService = planningVisiteService;
            _scopes = configuration.GetValue<string>("Api:Scopes").Split(' ');
            this._tokenAcquisition = tokenAcquisition;
            _authentificationService = authentificationService;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _planningVisiteService = planningVisiteService;
        }
        [Authorize]
        public async Task<IActionResult> Parametrage()
        {
            ViewData["clients"] = new SelectList(await _planningVisiteService.GetListeClients(), "Client_Id", "Client_RaisonSociale");
            HttpContext.Session.Clear();
            var userobjectId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))!.Value;
            //var listEntities = await _authentificationService.GetUserEntities(userobjectId);
            var userDb = await _authentificationService.GetUserInfos(userobjectId);
           /* HttpContext.Session.SetString("oid", value: userobjectId);
            HttpContext.Session.SetString("picture", value: string.IsNullOrEmpty(userDb.NetUser!.ProfilePicture!) ? "" : userDb.NetUser!.ProfilePicture!);
            HttpContext.Session.SetString("nom", value: userDb.NetUser!.FirstName!);
            HttpContext.Session.SetString("prenom", value: userDb.NetUser!.LastName!);
            var role = userDb.NetUser!.Roles.FirstOrDefault();*/
            return RedirectToAction("planificationDesVisites", "Viste");
            //return RedirectToAction("Index");

        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
