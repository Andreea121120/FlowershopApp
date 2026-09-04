using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Florarie.BLL
{
    public class BLLComenziClient
    {
        FlorarieEntities3 bd;
        public BLLComenziClient()
        {
            bd= new FlorarieEntities3();
        }

        public List<Comenzi> AfiseazaComenziPeClient(int idUtilizator)
        {
            Clienti client = bd.Clienti.FirstOrDefault(c => c.IdUtilizator == idUtilizator);
            if (client != null)
            {
                return bd.Comenzi.Where(c => c.IdClient == client.IdClient).ToList();
            }
            return new List<Comenzi>();
        }

    }
}