using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Florarie.DAL;

namespace Florarie.BLL
{
    public class BLLAutentificare { 
    
        private FlorarieEntities3 bd;
        public BLLAutentificare()
        {
            bd = new FlorarieEntities3();
        }

        public Utilizatori Autentificare(Utilizatori utilizator)
        {
            return bd.Utilizatori
                .FirstOrDefault(u => u.UserName == utilizator.UserName && u.Password == utilizator.Password);
        }


    }

}