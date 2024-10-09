using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france_2.Data;
using Vin_de_la_france_2.Models;

namespace Vin_de_la_france_2.Controllers
{
    public class ArticlesClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArticlesClasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArticlesClasses.Include(a => a.FamillesClass).Include(a => a.FournisseursClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArticlesClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articlesClass = await _context.ArticlesClasses
                .Include(a => a.FamillesClass)
                .Include(a => a.FournisseursClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articlesClass == null)
            {
                return NotFound();
            }

            return View(articlesClass);
        }

        // GetArticlesImages
        public IActionResult GetArticleImage(int id)
        {
            // Récupérer l'article avec l'image
            var article = _context.ArticlesClasses.FirstOrDefault(a => a.Id == id);

            if (article == null || string.IsNullOrEmpty(article.Image))
            {
                return NotFound(); // Si l'article n'a pas d'image, retourner 404
            }

            // Convertir la chaîne Base64 en tableau de bytes
            byte[] imageBytes = Convert.FromBase64String(article.Image);

            // Retourner l'image avec le bon type MIME (adaptez selon le format de l'image)
            return File(imageBytes, "image/png"); // Si vous utilisez PNG, sinon changez en "image/jpeg", etc.
        }


        private bool ArticlesClassExists(int id)
        {
            return _context.ArticlesClasses.Any(e => e.Id == id);
        }
    }
}
