using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vin_de_la_france.Data;  // Assurez-vous que ce namespace correspond à celui de votre ApplicationDbContext
using Vin_de_la_france.Models;

namespace Vin_de_la_france.Controllers
{
    public class VinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action pour afficher la liste des vins
        public async Task<IActionResult> List()
        {
            // Récupération des données depuis ArticlesClass avec les relations (Famille, Fournisseur)
            var vins = await _context.ArticlesClass
                .Include(v => v.FamillesClass)        // S'assurer d'inclure les relations avec Famille
                .Include(v => v.FournisseursClass)    // S'assurer d'inclure les relations avec Fournisseur
                .ToListAsync();

            return View(vins);  // Transmettre les données à la vue
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
