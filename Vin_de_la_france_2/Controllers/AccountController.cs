using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Vin_de_la_france_2.Models;
using System.Linq;
using Vin_de_la_france_2.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Vin_de_la_france_2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var client = _context.ClientsClass.FirstOrDefault(c => c.Email == email && c.Password == password);
            if (client != null)
            {
                HttpContext.Session.SetString("ClientName", client.Name);
                HttpContext.Session.SetString("ClientId", client.Id.ToString());

                return RedirectToAction("Index", "ArticlesClasses");
            }

            ModelState.AddModelError(string.Empty, "Adresse email ou mot de passe incorrect.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ClientName");
            HttpContext.Session.Remove("ClientId");

            return RedirectToAction("Index", "ArticlesClasses");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ClientsClass client)
        {
            if (ModelState.IsValid)
            {
                _context.ClientsClass.Add(client);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }
            return View(client);
        }
    }
}
