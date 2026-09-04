using Florarie.BLL;
using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Florarie.Controllers
{
    public class CarduriController : Controller
    {
        BLLCarduri bll;

        public CarduriController()
        {
            bll = new BLLCarduri();
        }
        // GET: Carduri
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Adauga()
        {
            Carduri card = new Carduri();
            return View(card);
        }

        [HttpPost]
        public ActionResult Adauga(Carduri card)
        {
            if (!ModelState.IsValid)
            {
                return View(card);
            }
          
            try
            {
                bll.AdaugaCard(card);
                return RedirectToAction("Index", "Comanda");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(card);
            }
        }
    }
}