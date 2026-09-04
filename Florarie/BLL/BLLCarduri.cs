using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Florarie.BLL
{
    public class BLLCarduri
    {
        FlorarieEntities3 bd;
        public BLLCarduri() 
        { 
            bd= new FlorarieEntities3();
        }

        public void AdaugaCard(Carduri card)
        {
            int? idUtilizator = HttpContext.Current.Session["IdUtilizator"] as int?;

            if (idUtilizator.HasValue)
            {
                BLLClienti bllClienti = new BLLClienti();
                int? idClient = bllClienti.GetIdClientByIdUtilizator(idUtilizator.Value);

                if (idClient.HasValue)
                {
                    bool cardExistent = bd.Carduri.Any(c => c.IdClient == idClient.Value);
                    if (cardExistent)
                    {
                        throw new InvalidOperationException("Clientul are deja un card înregistrat.");
                    }
                    card.IdClient = idClient.Value; 
                }
                else
                {
                    throw new InvalidOperationException("Clientul nu a fost găsit pentru utilizatorul curent.");
                }
            }
            else
            {
                throw new InvalidOperationException("Utilizatorul nu este logat.");
            }
                bd.Carduri.Add(card);
                bd.SaveChanges();

        }

        public List<Carduri> GetCarduriByIdUtilizator(int idUtilizator)
        {
            BLLClienti bllClienti = new BLLClienti();
            int? idClient = bllClienti.GetIdClientByIdUtilizator(idUtilizator);

            if (idClient.HasValue)
            {
                return bd.Carduri.Where(c => c.IdClient == idClient.Value).ToList();
            }
            return new List<Carduri>();
        }
    }
}