using Florarie.BLL;
using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Florarie.Controllers
{
    public class ClientiController : Controller
    {
        // GET: Clienti
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Adauga()
        {
            Clienti Cnou = new Clienti();
            return View(Cnou);
        }

        [HttpPost]
        public ActionResult Adauga(Clienti cNou)
        {
            if (Session["IdUtilizator"] != null)
            {
                cNou.IdUtilizator = (int)Session["IdUtilizator"]; 
            }
            else
            {

                ModelState.AddModelError("", "Utilizatorul nu este logat.");
                return View(cNou);
            }

            if (!ModelState.IsValid)
            {
                return View(cNou);
            }

            BLLClienti bllClienti = new BLLClienti();
            try
            {
                bllClienti.AdaugaClient(cNou);
                return RedirectToAction("Index", "Comanda");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(cNou); 
            }
        }

        public ActionResult VerificaSiRedirectioneaza()
        {
            if (Session["IdUtilizator"] != null)
            {
                int idUtilizator = (int)Session["IdUtilizator"];
                BLLClienti bllClienti = new BLLClienti();

                if (bllClienti.ClientExistent(idUtilizator))
                {
                    return RedirectToAction("PaginaProfil", "ProfilUtilizator");
                }
                else
                {
                    ViewBag.Message = "Nu ai un profil de client. Te rugăm să te înregistrezi.";
                    return View();
                }

            }

            return View();
        }

    }
}