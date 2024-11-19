using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FRIchat.Models;

namespace FRIchat.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        if (Utilities.Cookies.CookieExists(_httpContextAccessor))
        {
            ViewData["IsLoggedIn"] = true;
        }
        else
        {
            ViewData["IsLoggedIn"] = false;
        }
        
        return View("Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Register()
    {
        return RedirectToAction("Create", "Uporabnik");
    }
    
    public IActionResult Login()
    {
        return RedirectToAction("Login", "Uporabnik");
    }


    public IActionResult Logout()
    {
        Utilities.Cookies.ClearCookie(_httpContextAccessor);
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
