using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Florarie.DAL;

namespace Florarie.BLL
{
    
    public class BLLPlata
    {
        FlorarieEntities3 bd;

        public BLLPlata()
        {
            bd = new FlorarieEntities3();
        }

        public Plati GetPlataByIdComanda(int idComanda)
        {
            return bd.Plati.FirstOrDefault(p => p.IdComanda == idComanda);
        }
    }
}