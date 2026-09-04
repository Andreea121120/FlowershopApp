using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Florarie.DAL;

namespace Florarie.BLL
{
    public class BLLProduse
    {
        private FlorarieEntities3 bd;

        public BLLProduse()
        {
            bd = new FlorarieEntities3();
        }
       public void AdaugaInCos(int idUtilizator, int idProdus, int cantitate)
        {
            Utilizatori utilizator = bd.Utilizatori.FirstOrDefault(u => u.IdUtilizator == idUtilizator);
            if (utilizator == null)
            {
                throw new Exception("IdUtilizator nu există în tabelul Utilizatori.");
            }

            Cos produsInCos = bd.Cos.FirstOrDefault(c => c.IdProdus == idProdus && c.IdUtilizator == idUtilizator);

            if (produsInCos != null)
            {
                produsInCos.Cantitate += cantitate;
            }
            else
            {
                Cos cosNou = new Cos
                {
                    IdUtilizator = idUtilizator,
                    IdProdus = idProdus,
                    Cantitate = cantitate
                };
                bd.Cos.Add(cosNou);
            }
            bd.SaveChanges();
        }

        public Cos VerificaProdusInCos(int idUtilizator, int idProdus)
        {
            FlorarieEntities3 bd = new FlorarieEntities3();
            {
                int idCategorie = bd.Produse
                        .Where(p => p.IdProdus == idProdus)
                        .Select(p => p.IdCategorie)
                        .FirstOrDefault();
                return bd.Cos.FirstOrDefault(c => c.IdUtilizator == idUtilizator && c.IdProdus == idProdus);
            }
        }

        public void ActualizeazaCantitate(int idUtilizator, int idProdus, int cantitateNoua)
        {
            FlorarieEntities3 bd = new FlorarieEntities3();
            {
                Cos produsInCos = bd.Cos.FirstOrDefault(c => c.IdUtilizator == idUtilizator && c.IdProdus == idProdus);

                if (produsInCos != null)
                {
                    produsInCos.Cantitate = cantitateNoua;
                    bd.SaveChanges();
                }
            }
        }

        public Produse GetIdProdus(int id)
        {
            FlorarieEntities3 bd = new FlorarieEntities3 ();
            {
                return bd.Produse.FirstOrDefault(p => p.IdProdus == id);
            }
        }

        public List<Produse> produseCategorie(string categorie)
        {
            return bd.Produse.Where(p => p.CategoriiProduse.DenumireCategorie == categorie).ToList();
        }

        public Produse produsId(int id)
        {
            return bd.Produse.FirstOrDefault(p => p.IdProdus == id);
        }

        public string CategorieId(int id)
        {
            CategoriiProduse categorie = bd.CategoriiProduse.FirstOrDefault(c => c.IdCategorie == id);
            return categorie != null ? categorie.DenumireCategorie : "Nu s-a gasit categoria";
        }
    }
}