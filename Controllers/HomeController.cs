using LAB01_ED1_G.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAB01_ED1_G.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create(string lista)
        {
            if (lista == "s")
            {
                return RedirectToAction("Index", "Single");
            }
            else
            {
                return RedirectToAction("Index", "Double");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}