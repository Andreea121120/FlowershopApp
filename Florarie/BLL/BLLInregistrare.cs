using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Florarie.DAL;

namespace Florarie.BLL
{
    public class BLLInregistrare
    {
        private FlorarieEntities3 bd;
        public BLLInregistrare()
        {
            bd = new FlorarieEntities3();
        }
        public void AdaugaUtilizator(Utilizatori uNou)
        {
            bd.Utilizatori.Add(uNou);
            bd.SaveChanges();

            int idUtilizatorNou = uNou.IdUtilizator; 
            AdaugaRolUtilizator(idUtilizatorNou, 2);
        }

        public void AdaugaRolUtilizator(int idUtilizator, int idRol)
        {
            RoluriUtilizatori rolUtilizatorNou = new RoluriUtilizatori
            {
                IdUtilizator = idUtilizator,
                IdRol = idRol,
                DataAcordare = DateTime.Now
            };

            bd.RoluriUtilizatori.Add(rolUtilizatorNou);
            bd.SaveChanges();
        }

        public bool VerificareU(string userName)
        {
            Utilizatori utilizatorExistent = bd.Utilizatori.FirstOrDefault(u => u.UserName == userName);
            return utilizatorExistent != null;
        }

    }
}