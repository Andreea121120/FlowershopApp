using Florarie.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Florarie.BLL
{
   
    public class BLLClienti
    {
        private FlorarieEntities3 bd;

        public BLLClienti() { 
            bd= new FlorarieEntities3();
        }
        public void AdaugaClient(Clienti cNou)
        {
            Clienti clientExistent = bd.Clienti.FirstOrDefault(c => c.IdUtilizator == cNou.IdUtilizator);
            if (clientExistent != null)
            {
                throw new InvalidOperationException("Un client cu acest IdUtilizator există deja.");
            }
                bd.Clienti.Add(cNou);
                bd.SaveChanges();
        }
        public bool ClientExistent(int idUtilizator)
        {
            return bd.Clienti.Any(c => c.IdUtilizator == idUtilizator);
        }

        public Clienti GetClientById(int idUtilizator)

        {
            return bd.Clienti.FirstOrDefault(c => c.IdUtilizator == idUtilizator);
        }

        public int? GetIdClientByIdUtilizator(int idUtilizator)
        {
            Clienti client = bd.Clienti.FirstOrDefault(c => c.IdUtilizator == idUtilizator);
            return client?.IdClient; 
        }
        public void ModificareProfil(Clienti client)
        {
            Clienti clientExistent = bd.Clienti.FirstOrDefault(c => c.IdClient == client.IdClient);
            if (clientExistent == null)
            {
                throw new InvalidOperationException("Clientul nu a fost găsit.");
            }

            clientExistent.Nume = client.Nume;
            clientExistent.Prenume = client.Prenume;
            clientExistent.Adresa = client.Adresa;
            clientExistent.Telefon = client.Telefon;
            clientExistent.Email = client.Email;
            clientExistent.Localitate = client.Localitate;

            bd.SaveChanges();
        }
    }

}