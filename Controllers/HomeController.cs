using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FRIchat.Models;

namespace FRIchat.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
<<<<<<< HEAD
        if (Utilities.Cookies.CookieExists(_httpContextAccessor))
        {
            ViewData["IsLoggedIn"] = true;
        }
        else
        {
            ViewData["IsLoggedIn"] = false;
        }
        
        return View("Home");
=======
        return View();
>>>>>>> cb4f5730b253e5a9369e0e883cf7c9ae6f168a76
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Register()
    {
        return Redirect($"/Uporabnik/Create");
    }
    
    public IActionResult Login()
    {
<<<<<<< HEAD
        return RedirectToAction("Login", "Uporabnik");
    }


    public IActionResult Logout()
    {
        Utilities.Cookies.ClearCookie(_httpContextAccessor);
        return RedirectToAction("Index", "Home");
=======
        return Redirect($"/Uporabnik/Login");
>>>>>>> cb4f5730b253e5a9369e0e883cf7c9ae6f168a76
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
