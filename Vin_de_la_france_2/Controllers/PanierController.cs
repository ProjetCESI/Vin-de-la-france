using Microsoft.AspNetCore.Mvc;
using Vin_de_la_france_2.Models;
using Vin_de_la_france_2.Services;

namespace Vin_de_la_france_2.Controllers
{
    public class PanierController : Controller
    {
        private readonly PanierService _panierService;

        public PanierController(PanierService panierService)
        {
            _panierService = panierService;
        }

        public IActionResult Index()
        {
            var panier = _panierService.GetPanier();
            if (panier != null && panier.LigneCommandeClientsClass != null)
            {
                Console.WriteLine($"Panier récupéré avec {panier.LigneCommandeClientsClass.Count} articles.");
            }
            else
            {
                Console.WriteLine("Aucun panier trouvé ou LigneCommandeClientsClass est null.");
            }
            return View("Panier", panier);
        }


        [HttpPost]
        public IActionResult AjouterAuPanier(int articleId, int quantite = 1)
        {
            Console.WriteLine($"Appel de AjouterAuPanier avec articleId: {articleId} et quantite: {quantite}");

            if (quantite < 1)
            {
                Console.WriteLine("Quantité invalide, elle doit être au moins de 1.");
                ModelState.AddModelError("", "La quantité doit être au moins 1.");
                return RedirectToAction("Details", new { id = articleId });
            }

            _panierService.AjouterArticle(articleId, quantite);

            Console.WriteLine("Redirection vers la vue Panier après ajout.");

            return RedirectToAction("Index");
        }





        public IActionResult MettreAJourQuantite(int ligneCommandeId, int quantite)
        {
            _panierService.MettreAJourQuantite(ligneCommandeId, quantite);
            return RedirectToAction("Index");
        }

        public IActionResult SupprimerDuPanier(int ligneCommandeId)
        {
            _panierService.SupprimerArticle(ligneCommandeId);
            return RedirectToAction("Index");
        }

        public IActionResult ValiderCommande()
        {
            _panierService.ValiderCommande();
            return RedirectToAction("Index");
        }


    }
}
