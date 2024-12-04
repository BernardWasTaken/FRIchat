using FRIchat.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRIchat.Controllers;

public class DashboardController : Controller
{
    private readonly FRIchatContext _context;
    public DashboardController(FRIchatContext context)
    {
        _context = context;
    }
    [Authorize]
    // GET
    public IActionResult Index()
    {
        var objave = _context.Objava.ToList();
        return View();
    }
}