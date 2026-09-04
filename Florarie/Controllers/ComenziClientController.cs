using Florarie.BLL;
using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Florarie.Controllers
{
    public class ComenziClientController : Controller
    {
        BLLComenziClient bll;
        public ComenziClientController()
        {
            bll= new BLLComenziClient();
        }
        // GET: ComenziClient
        public ActionResult Index()
        {
            int idUtilizator = (int)Session["IdUtilizator"];

            List<Comenzi> comenzi = bll.AfiseazaComenziPeClient(idUtilizator);
            return View(comenzi);
        }
    }
}