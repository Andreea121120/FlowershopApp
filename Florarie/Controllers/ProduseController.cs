
using Florarie.BLL;
using Florarie.DAL;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Florarie.Controllers
{
    public class ProduseController : Controller
    {
        BLLProduse obBLLProduse;

        public ProduseController()
        {
            obBLLProduse = new BLLProduse();
        }

        [HttpPost]
        public ActionResult AdaugaInCos(int idProdus, int cantitate)
        {
            if (Session["IdUtilizator"] == null)
            {
                return RedirectToAction("Autentificare", "Autentificare");
            }

            int idUtilizator = Convert.ToInt32(Session["IdUtilizator"]);

            Cos produsInCos = obBLLProduse.VerificaProdusInCos(idUtilizator, idProdus);
            
            if (produsInCos != null)
            {
                obBLLProduse.ActualizeazaCantitate(idUtilizator, idProdus, produsInCos.Cantitate + cantitate);
            }
            else
            {
                obBLLProduse.AdaugaInCos(idUtilizator, idProdus, 1);
            }

            return RedirectToAction("Index", "CosCumparaturi");
        }

        public ActionResult Paginare(string categorie, int pagina=1, int pagSize=9)
        {
            List<Produse> produse = obBLLProduse.produseCategorie(categorie);

            int nrProduse = produse.Count;
            var produsePagina = produse.Skip((pagina - 1) * pagSize).Take(pagSize).ToList();

            ViewBag.Categorie = categorie;
            ViewBag.NrPagini = (int)Math.Ceiling((double)nrProduse / pagSize);
            ViewBag.PaginaCurenta = pagina;

            return View(produsePagina);
        }
        public ActionResult Buchete(int pagina=1)
        {
            List<Produse> produse = obBLLProduse.produseCategorie("Buchete");
            ViewBag.Produse = produse;
            return Paginare("Buchete", pagina, 9);
        }


        public ActionResult Aranjamente(int pagina=1)
        {
            List<Produse> produse = obBLLProduse.produseCategorie("Aranjamente");
            ViewBag.Produse = produse;
            return Paginare("Aranjamente", pagina, 9);
        }

        public ActionResult Plante(int pagina=1)
        {
            List<Produse> produse = obBLLProduse.produseCategorie("Plante");
            ViewBag.Produse = produse;
            return Paginare("Plante", pagina, 9);
        }

        public ActionResult Cadouri(int pagina=1)
        {
            List<Produse> produse = obBLLProduse.produseCategorie("Cadouri");
            ViewBag.Produse = produse;
            return Paginare("Cadouri", pagina, 9);
        }

        public ActionResult Detalii(int id)
        {
            Produse produs = obBLLProduse.produsId(id);
            if (produs == null)
            {
                return HttpNotFound();
            }
            string denumireCategorie = obBLLProduse.CategorieId(produs.IdCategorie);

            ViewBag.DenumireCategorie = denumireCategorie;

            return View(produs);
        }
        
    }
}