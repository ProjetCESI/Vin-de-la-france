using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vin_de_la_france.Models;

namespace Vin_de_la_france.Controllers
{
    public class VinsController : Controller
    {
        private readonly ILogger<VinsController> _logger;

        public VinsController(ILogger<VinsController> logger)
        {
            _logger = logger;
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
