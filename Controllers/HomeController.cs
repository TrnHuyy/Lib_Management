using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;

namespace Lib2.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LibraryContext _context;

    public HomeController(ILogger<HomeController> logger, LibraryContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View(books);
    }

    public IActionResult ViewUsers()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
