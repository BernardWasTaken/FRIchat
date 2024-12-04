using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FRIchat.Models;
using FRIchat.Data;  // Dodajte ta prostor imen za dostop do vašega DbContext

namespace FRIchat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FRIchatContext _context;  // Dodajte kontekst

        // Konstruktor, ki sprejme DbContext
        public HomeController(ILogger<HomeController> logger, FRIchatContext context)
        {
            _logger = logger;
            _context = context;  // Inicializirajte kontekst
        }

        // Akcija Index, ki vrne seznam predmetov
        public IActionResult Index()
        {
            var predmeti = _context.Predmet.ToList();  // Pridobite seznam predmetov iz baze
            return View(predmeti);  // Pošljite seznam predmetov v pogled
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Register()
        {
            return Redirect($"/Uporabnik/Create");
        }

        public IActionResult Login()
        {
            return Redirect($"/Uporabnik/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
