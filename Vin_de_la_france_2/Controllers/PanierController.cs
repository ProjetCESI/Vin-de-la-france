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
            return View("Panier", panier);
        }


        [HttpPost]
        public IActionResult AjouterAuPanier(int articleId, int quantite)
        {
            var clientIdString = HttpContext.Session.GetString("ClientId");
            if (string.IsNullOrEmpty(clientIdString))
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour ajouter des articles au panier.";
                return RedirectToAction("Login", "Account");
            }

            int clientId = int.Parse(clientIdString);

            TempData["SuccessMessage"] = "L'article a été ajouté au panier avec succès.";
            return RedirectToAction("Index", "Panier");
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
