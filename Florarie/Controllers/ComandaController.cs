using Florarie.BLL;
using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Florarie.Controllers
{
    public class ComandaController : Controller
    {
        // GET: Comanda
        BLLCos bll;

        public ComandaController() {
        bll = new BLLCos();
        }
        public ActionResult Index()
        {
            int idUtilizator = (int)Session["IdUtilizator"]; 
            BLLClienti bllClienti = new BLLClienti();
            Clienti client = bllClienti.GetClientById(idUtilizator);
            Session["Client"] = client;

            List<ProduseCos_Result> listaProduse = bll.AfiseazaProduseCos(idUtilizator);
            decimal sumaTotala = listaProduse.Sum(p => p.ValoareTotala);
            ViewBag.SumaTotala = sumaTotala;

            return View(listaProduse);
        }

        public ActionResult FinalizeazaComanda()
        {
            int idUtilizator = (int)Session["IdUtilizator"];
            BLLCos bll = new BLLCos();
            List<ProduseCos_Result> listaProduse = bll.AfiseazaProduseCos(idUtilizator);
            decimal sumaTotala = listaProduse.Sum(p => p.ValoareTotala);

            BLLClienti bllClienti = new BLLClienti();
            Clienti client = bllClienti.GetClientById(idUtilizator);
            if (client == null)
            {
                ModelState.AddModelError("", "Clientul nu există.");
                return RedirectToAction("Index");
            }

            BLLCarduri bllCarduri = new BLLCarduri();
            List<Carduri> carduri = bllCarduri.GetCarduriByIdUtilizator(idUtilizator);
            int idCard = carduri.FirstOrDefault()?.IdCard ?? 0; 

            if (idCard == 0)
            {
                ModelState.AddModelError("", "Utilizatorul nu are un card asociat.");
                return RedirectToAction("Index");
            }

            BLLFinalizareComanda bllFinalizare = new BLLFinalizareComanda();
            bllFinalizare.SalveazaComanda(client.IdClient, sumaTotala);

            int idComanda = bllFinalizare.UltimulIdComandaInserat();

            bllFinalizare.SalveazaPlata(idComanda, sumaTotala, idCard);

            bll.StergeProdusLista(idUtilizator);

            return RedirectToAction("Index", "Plata", new { idComanda = idComanda });

        }

    }
}