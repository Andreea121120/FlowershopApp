using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Florarie.BLL;
using Florarie.DAL;

namespace Florarie.Controllers
{
    public class InregistrareController : Controller
    {
        
        // GET: Inregistrare
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Adauga()
        {
            Utilizatori Unou = new Utilizatori();
            return View(Unou);
        }

        [HttpPost]
        public ActionResult Adauga(Utilizatori uNou)
        {
            if (string.IsNullOrEmpty(uNou.UserName) || string.IsNullOrEmpty(uNou.Password) || string.IsNullOrEmpty(uNou.Email))
            {
                ModelState.AddModelError("", "Toate câmpurile sunt obligatorii.");
                return View(uNou);
            }

            BLLInregistrare bll = new BLLInregistrare();

            bool utilizatorExista = bll.VerificareU(uNou.UserName);
            if (utilizatorExista)
            {
                ModelState.AddModelError("", "Utilizatorul există deja.");
                return View(uNou);
            }

            try
            {
                bll.AdaugaUtilizator(uNou);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError("", "A apărut o eroare la salvarea utilizatorului.");
                return View(uNou);
            }
        }
    }
}
