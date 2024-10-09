using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vin_de_la_france.Data;
using Vin_de_la_france.Models;
using Vin_de_la_france.Models.Entities; // Corrige l'espace de noms pour inclure Entities

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
    var vins = await _context.Vins
        .Include(v => v.Famille)        // S'assurer d'inclure les relations
        .Include(v => v.Fournisseur)
        .ToListAsync();

    return View(vins);
}


        // Action pour afficher les détails d'un vin
        public async Task<IActionResult> Details(int id)
        {
            var vin = await _context.Vins
                .Include(v => v.Famille)
                .Include(v => v.Fournisseur)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vin == null)
            {
                return NotFound();
            }

            return View(vin);
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
