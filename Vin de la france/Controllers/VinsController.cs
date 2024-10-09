using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vin_de_la_france.Data; 
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

        public async Task<IActionResult> List()
        {
            var articles = await _context.ArticlesClass
                .Include(a => a.FamillesClass) 
                .Include(a => a.FournisseursClass) 
                .ToListAsync();

            return View(articles); 
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
