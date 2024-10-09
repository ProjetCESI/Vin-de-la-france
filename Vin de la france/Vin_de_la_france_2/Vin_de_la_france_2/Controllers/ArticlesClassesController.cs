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

        // GET: ArticlesClasses/Create
        public IActionResult Create()
        {
            ViewData["FamillesClassId"] = new SelectList(_context.FamillesClasses, "Id", "Name");
            ViewData["FournisseursClassId"] = new SelectList(_context.FournisseursClasses, "Id", "Name");
            return View();
        }

        // POST: ArticlesClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitPrice,QuantityStock,MinimumThreshold,Reference,FamillesClassId,FournisseursClassId")] ArticlesClass articlesClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articlesClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamillesClassId"] = new SelectList(_context.FamillesClasses, "Id", "Name", articlesClass.FamillesClassId);
            ViewData["FournisseursClassId"] = new SelectList(_context.FournisseursClasses, "Id", "Name", articlesClass.FournisseursClassId);
            return View(articlesClass);
        }

        // GET: ArticlesClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articlesClass = await _context.ArticlesClasses.FindAsync(id);
            if (articlesClass == null)
            {
                return NotFound();
            }
            ViewData["FamillesClassId"] = new SelectList(_context.FamillesClasses, "Id", "Name", articlesClass.FamillesClassId);
            ViewData["FournisseursClassId"] = new SelectList(_context.FournisseursClasses, "Id", "Name", articlesClass.FournisseursClassId);
            return View(articlesClass);
        }

        // POST: ArticlesClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UnitPrice,QuantityStock,MinimumThreshold,Reference,FamillesClassId,FournisseursClassId")] ArticlesClass articlesClass)
        {
            if (id != articlesClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articlesClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticlesClassExists(articlesClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamillesClassId"] = new SelectList(_context.FamillesClasses, "Id", "Name", articlesClass.FamillesClassId);
            ViewData["FournisseursClassId"] = new SelectList(_context.FournisseursClasses, "Id", "Name", articlesClass.FournisseursClassId);
            return View(articlesClass);
        }

        // GET: ArticlesClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ArticlesClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articlesClass = await _context.ArticlesClasses.FindAsync(id);
            if (articlesClass != null)
            {
                _context.ArticlesClasses.Remove(articlesClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticlesClassExists(int id)
        {
            return _context.ArticlesClasses.Any(e => e.Id == id);
        }
    }
}
