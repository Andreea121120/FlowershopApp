using Florarie.BLL;
using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Florarie.Controllers
{
    public class PlataController : Controller
    {
        BLLPlata bllPlata;
        BLLCarduri bllCarduri;

        public PlataController()
        {
            bllPlata = new BLLPlata();
            bllCarduri = new BLLCarduri();
        }

        public ActionResult Index(int idComanda)
        {
            Plati plata = bllPlata.GetPlataByIdComanda(idComanda);

            if (plata == null)
            {
                ModelState.AddModelError("", "Nu există o plată asociată acestei comenzi.");
                return RedirectToAction("Index", "Home");
            }

            int? idUtilizator = HttpContext.Session["IdUtilizator"] as int?;
            if (idUtilizator.HasValue)
            {
                List<Carduri> carduri = bllCarduri.GetCarduriByIdUtilizator(idUtilizator.Value);
                ViewBag.Carduri = carduri;
                if (carduri != null && carduri.Any())
                {
                    plata.IdCard = carduri.First().IdCard;
                }
            }
            return View(plata);
        }
    }
}