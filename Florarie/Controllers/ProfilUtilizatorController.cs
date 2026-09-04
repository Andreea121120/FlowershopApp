using Florarie.DAL;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Florarie.BLL;

namespace Florarie.Controllers
{
    public class ProfilUtilizatorController : Controller
        
    {
        // GET: ProfilUtilizator
        
        public ActionResult Index()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Deconectare()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PaginaProfil()
        {
            if (Session["IdUtilizator"] != null)
            {
                int idUtilizator = (int)Session["IdUtilizator"];
                BLLClienti bllClienti = new BLLClienti();
                Clienti client = bllClienti.GetClientById(idUtilizator);

                if (client == null)
                {
                    return RedirectToAction("Autentificare", "Autentificare");
                }

                return View(client);
            }
            return View();
        }

        public ActionResult _PaginaProfil()
        {
            if (Session["IdUtilizator"] != null)
            {
                int idUtilizator = (int)Session["IdUtilizator"];
                BLLClienti bllClienti = new BLLClienti();

                Clienti client = bllClienti.GetClientById(idUtilizator);

                if (client == null)
                {
                    return PartialView("_PaginaProfil", null);
                }

                return PartialView("_PaginaProfil", client);
            }
            else
            {
                return RedirectToAction("Autentificare", "Autentificare");
            }
        }

        public ActionResult Edit()
        {
            int userId = (int)Session["IdUtilizator"];  

            BLLClienti bllClienti = new BLLClienti();
            Clienti client = bllClienti.GetClientById(userId); 

            if (client == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(client); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clienti client)
        {

            if (ModelState.IsValid)
            {
                BLLClienti bllClienti = new BLLClienti();
                try
                {
                    bllClienti.ModificareProfil(client);
                    return RedirectToAction("PaginaProfil");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(client);
        }
    }
}