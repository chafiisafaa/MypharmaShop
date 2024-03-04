using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.IServices;
using Web.Models;

namespace Web.Controllers
{
    public class VisteController : Controller
    {
        private readonly IPlanningVisiteService _planningVisiteService;



        public VisteController(IPlanningVisiteService planningVisiteService)
        {
            _planningVisiteService = planningVisiteService;

        }
        // GET: VisteController
        public async Task<ActionResult> planificationDesVisites(PVVM pvVM)
        {

            ViewData["clients"] = new SelectList(await _planningVisiteService.GetListeClients(), "Client_Id", "Client_RaisonSociale");
            return View();
        }
        public async Task<ActionResult> Visites(PVVM pvVM)
        {
            var data = _planningVisiteService.GetListePlanning(pvVM.nvId, pvVM.userId, pvVM.clientId,pvVM.dateDebut, pvVM.dateFin, pvVM.etat);
            pvVM.pvs = data;
            ViewData["visiteurs"] = new SelectList(_planningVisiteService.GetListeUsers(), "Id", "UserName");
            ViewData["nvs"] = new SelectList(_planningVisiteService.GetListeRoles(), "Id", "Name");
            ViewData["clients"] = new SelectList(await _planningVisiteService.GetListeClients(), "Client_Id", "Client_RaisonSociale");
            return View(pvVM);
        }
        [HttpPost]
        public async Task<ActionResult> AddPlanningViste(PVVM pvVM)
        {
            pvVM.pv.PlanningVisite_Actif = true;
            var redirect = await _planningVisiteService.AddPlanningViste(pvVM.pv);
            if (redirect)
            {
                TempData["create"] = 1;
                //return Json(new { success = true,userId=pvVM.pv.PlanningVisite_userId });
                return RedirectToAction("planificationDesVisites");
            }
            TempData["create"] = 0;
            //return Json(new { success = false });
            //return View("planificationDesVisites", pvVM);
            return RedirectToAction("planificationDesVisites");
            //return Json(new { success = true });

        }
        [HttpGet]
        public List<PlanningVisite> GetListePlanning(string userId)
        {
            List<PlanningVisite> data = new();
            try
            {
                data = _planningVisiteService.GetListePlanning(userId);
                return data;
            }
            catch(Exception ex)
            {
                return data;
            }

        }
        [HttpPost]
        public async Task<IActionResult> ModifierPlanningViste(PVVM pvVM)
        {
            
            var redirect = await _planningVisiteService.UpdatePlanningViste(pvVM.pvUpdate);

            /*if (redirect)
            {
                TempData["updatePv"] = 1;
                return RedirectToAction("Visites");
            }
            TempData["updatePv"] = 0;
            return RedirectToAction("Visites");*/
            if (redirect)
            {
                TempData["updatePv"] = 1;
                return RedirectToAction("planificationDesVisites");
            }
            TempData["updatePv"] = 0;
            return RedirectToAction("planificationDesVisites");

            

        }
        [HttpPost]
        public async Task<bool> deletePv(int Id)
        {
            var result = await _planningVisiteService.DeletePlanningViste(Id);
            return result;

        }
        [HttpGet]
        public async Task<List<AspNetUserRoles>> GetListUsers(string roleId)
        {
            var result= await _planningVisiteService.GetListUsers(roleId);
            return result;
        }


    }
}
