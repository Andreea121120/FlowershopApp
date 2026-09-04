using Florarie.BLL;
using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Florarie.Controllers
{
    public class AutentificareController : Controller
    {
        // GET: Autentificare
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Autentificare()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autentificare(Utilizatori utilizator)
        {
            BLLAutentificare lg = new BLLAutentificare();

            Utilizatori utilizatorGasit = lg.Autentificare(utilizator);

            if (utilizatorGasit != null)
            {

                Session["UserName"] = utilizatorGasit.UserName;
                Session["IdUtilizator"] = utilizatorGasit.IdUtilizator;

                FormsAuthentication.SetAuthCookie(utilizatorGasit.UserName, createPersistentCookie: false);

                return RedirectToAction("Index", "Home");
            }
            else if (string.IsNullOrEmpty(utilizator.UserName) || string.IsNullOrEmpty(utilizator.Password))
            {
                ModelState.AddModelError("", "Completati toate campurile.");
                return View("Autentificare", utilizator);
            }
            else
            {
                ModelState.AddModelError("", "Utilizator necunoscut.");
                return View("Autentificare", utilizator);
            }
        }
    }
}

