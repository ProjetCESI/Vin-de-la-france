using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Vin_de_la_france_2.Data;
using Vin_de_la_france_2.Models;

namespace Vin_de_la_france_2.Services
{
    public class PanierService
    {
        private readonly ApplicationDbContext _context;

        public PanierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetPanierCount()
        {
            var panier = GetPanier();
            if (panier == null || panier.LigneCommandeClientsClass == null || !panier.LigneCommandeClientsClass.Any())
            {
                return 0;
            }

            int totalQuantite = panier.LigneCommandeClientsClass.Sum(l => l.Quantite);
            return totalQuantite;
        }


        public CommandeClientsClass GetPanier()
        {
            var panier = _context.CommandeClientsClasses
                .Include(c => c.LigneCommandeClientsClass)
                .ThenInclude(l => l.ArticlesClass)
                .FirstOrDefault(c => c.Statut == "En cours");

            if (panier == null)
            {
                panier = new CommandeClientsClass
                {
                    Date = DateTime.Now,
                    Statut = "En cours",
                    ClientsClassId = 1
                };
                _context.CommandeClientsClasses.Add(panier);
                _context.SaveChanges();
            }

            return panier;
        }

        public void AjouterArticle(int articleId, int quantite)
        {

            var panier = GetPanier();

            var article = _context.ArticlesClasses.Find(articleId);
            if (article != null)
            {

                var ligneCommande = _context.LigneCommandeClientsClasses
                    .FirstOrDefault(lc => lc.CommandeClientsClassId == panier.Id && lc.ArticlesClassId == articleId);

                if (ligneCommande == null)
                {
                    ligneCommande = new LigneCommandeClientsClass
                    {
                        ArticlesClassId = articleId,
                        CommandeClientsClassId = panier.Id,
                        Quantite = quantite,
                        PrixUnitaire = article.UnitPrice
                    };
                    _context.LigneCommandeClientsClasses.Add(ligneCommande);
                }
                else
                {
                    ligneCommande.Quantite += quantite;
                }

                _context.SaveChanges();
            }
            else
            {
            }
        }


        public void MettreAJourQuantite(int ligneCommandeId, int quantite)
        {
            var ligneCommande = _context.LigneCommandeClientsClasses.Find(ligneCommandeId);
            if (ligneCommande != null)
            {
                ligneCommande.Quantite = quantite;
                _context.SaveChanges();
            }
        }

        public void SupprimerArticle(int ligneCommandeId)
        {
            var ligneCommande = _context.LigneCommandeClientsClasses.Find(ligneCommandeId);
            if (ligneCommande != null)
            {
                _context.LigneCommandeClientsClasses.Remove(ligneCommande);
                _context.SaveChanges();
            }
        }

        public void ValiderCommande()
        {
            var panier = GetPanier();
            panier.Statut = "Validée";
            _context.SaveChanges();
        }
    }
}
