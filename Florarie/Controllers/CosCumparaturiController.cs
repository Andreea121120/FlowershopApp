using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Florarie.BLL;
using Florarie.DAL;

namespace Florarie.Controllers
{
    
    public class CosCumparaturiController : Controller
    {
        BLLCos bLLCos;
        BLLProduse obBLLProduse;
        private FlorarieEntities3 bd;
        public CosCumparaturiController() {

            bLLCos = new BLLCos();
            obBLLProduse = new BLLProduse();
        }
        public ActionResult Index()
        {
            int idUtilizator = Convert.ToInt32(Session["IdUtilizator"]);

            List<ProduseCos_Result> listaProduse = bLLCos.AfiseazaProduseCos(idUtilizator);
            decimal sumaTotala = listaProduse.Sum(p => p.ValoareTotala);
            ViewBag.SumaTotala = sumaTotala;

            return View(listaProduse);
        }
     
        [HttpPost]
        public ActionResult Delete(int id)
        {
            BLLCos bllCos = new BLLCos();

            bllCos.StergeProdusCos(id);

            return RedirectToAction("Index");
        }
        public ActionResult PlaseazaComanda()
        {
            if (Session["IdUtilizator"] != null)
            {
                int idUtilizator = (int)Session["IdUtilizator"];
                BLLClienti bllClienti = new BLLClienti();

                if (!bllClienti.ClientExistent(idUtilizator))
                {

                    return RedirectToAction("Adauga", "Clienti");
                }

                List<ProduseCos_Result> listaProduse = bLLCos.AfiseazaProduseCos(idUtilizator);
                if (!listaProduse.Any())
                {   
                    TempData["ErrorMessage"] = "Coșul de cumpărături este gol. Adăugați produse înainte de a plasa comanda.";
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("Index", "Comanda");
                }
                
            }
            
            return View();
        }

    }
}