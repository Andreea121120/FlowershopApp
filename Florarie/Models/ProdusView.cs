using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Florarie.Models
{
    public class ProdusView
    {
        public int IdProdus { get; set; }
        public string NumeProdus { get; set; }
        public decimal Pret { get; set; }
        public string Imagine { get; set; }
        public string DenumireCategorie { get; set; }
    }
}