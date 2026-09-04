using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Florarie.BLL
{
    public class BLLFinalizareComanda
    {
        FlorarieEntities3 bd;

        public BLLFinalizareComanda()
        {
            bd = new FlorarieEntities3();
        }

        public List<ProduseCos_Result> AfiseazaProduseCos(int idUtilizator)
        {
            return bd.ProduseCos(idUtilizator).ToList();

        }

        public void SalveazaComanda(int idClient, decimal total)
        {
            Comenzi comanda = new Comenzi();
            {
                comanda.DataComanda = DateTime.Now;
                comanda.Total = total;
                comanda.IdClient = idClient;
            };

            bd.Comenzi.Add(comanda);
            bd.SaveChanges();
        }

        public void SalveazaPlata(int idComanda, decimal suma, int idCard)
        {

            Plati plata = new Plati();
            {
                plata.IdComanda = idComanda;
                plata.MetodaPlata = "Card";
                plata.Suma = suma;
                plata.DataPlata = DateTime.Now;
                plata.IdCard = idCard;
            };

            bd.Plati.Add(plata);
            bd.SaveChanges();
        }

        public int UltimulIdComandaInserat()
        {
            return bd.Comenzi.OrderByDescending(c => c.IdComanda).FirstOrDefault().IdComanda;
        }
    }
}