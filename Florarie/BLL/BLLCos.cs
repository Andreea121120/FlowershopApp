using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Florarie.DAL;

namespace Florarie.BLL
{
    public class BLLCos
    {
        FlorarieEntities3 bd;
        public BLLCos()
        {
            bd = new FlorarieEntities3();
        }

        public List<Cos> ListaProduseCos()
        {
            return bd.Cos.ToList();
        }

        public List<ProduseCos_Result> AfiseazaProduseCos(int idUtilizator)
        {
            return bd.ProduseCos(idUtilizator).ToList();

        }

        public void StergeProdusCos(int idProdus)
        {
 
            Cos produs = bd.Cos.FirstOrDefault(c => c.IdProdus == idProdus);
            if (produs != null)
            {
                bd.Cos.Remove(produs);
                bd.SaveChanges();
            }

   
        }
        public void StergeProdusLista(int idUtilizator)
        {
            List<Cos> produseCos = bd.Cos.Where(c => c.IdUtilizator == idUtilizator).ToList();

            if (produseCos.Any())
            {
                bd.Cos.RemoveRange(produseCos);
                bd.SaveChanges();
            }
        }

    }
}