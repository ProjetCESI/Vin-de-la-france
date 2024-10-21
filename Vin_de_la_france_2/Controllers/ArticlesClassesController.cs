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

        public async Task<IActionResult> Index(string searchName, int? familleId)
        {
            ViewBag.FamillesList = new SelectList(_context.FamillesClasses, "Id", "Name");

            var articles = _context.ArticlesClasses
                .Include(a => a.FamillesClass)
                .Include(a => a.FournisseursClass)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                articles = articles.Where(a => a.Name.Contains(searchName));
                ViewData["searchName"] = searchName;
            }

            if (familleId.HasValue && familleId.Value > 0)
            {
                articles = articles.Where(a => a.FamillesClassId == familleId.Value);
            }

            return View(await articles.ToListAsync());
        }

        public async Task<IActionResult> FilterArticles(string searchName, int? familleId, int? minPrice, int? maxPrice)
        {
            var articles = _context.ArticlesClasses
                .Include(a => a.FamillesClass)
                .Include(a => a.FournisseursClass)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                articles = articles.Where(a => a.Name.Contains(searchName));
            }

            if (familleId.HasValue && familleId.Value > 0)
            {
                articles = articles.Where(a => a.FamillesClassId == familleId.Value);
            }

            if (minPrice.HasValue)
            {
                articles = articles.Where(a => a.UnitPrice >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                articles = articles.Where(a => a.UnitPrice <= maxPrice.Value);
            }

            return PartialView("_ArticleCards", await articles.ToListAsync());
        }

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
        public IActionResult GetArticleImage(int id)
        {
            var article = _context.ArticlesClasses.FirstOrDefault(a => a.Id == id);

            if (article == null || string.IsNullOrEmpty(article.Image))
            {
                return NotFound();
            }

            byte[] imageBytes = Convert.FromBase64String(article.Image);
            return File(imageBytes, "image/png");
        }
    }
}
