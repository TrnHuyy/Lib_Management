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
    // Hiển thị danh sách các sách
    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View(books);
    }

    // Hiển thị ds người dùng
    public IActionResult ViewUsers()
    {
        var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
        if(isLibrarian == 1)
        {
        var users = _context.Users.ToList();
        return View(users);
        }
        else
        {
            return BadRequest("You're not Librarian => No Access");
        }
    }

    // Hiển thị ttin cá nhân
    [HttpGet]
    public IActionResult Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if( userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if(user == null)
        {
            return BadRequest("User Not Found");
        }

        return View(user);
    }

    [HttpGet]
    public IActionResult ChangeName()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ChangeName([FromForm]string name)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            user.ChangeName(name);
            _context.SaveChanges();
            return RedirectToAction("Profile", "Home");
        }
    }

    [HttpGet]
    public IActionResult ChangePass()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ChangePass([FromForm]string oldPass, [FromForm]string newPass)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if(user.Password != oldPass)
        {
            return BadRequest("Old Password Incorrect");
        }
        user.ChangePass(newPass);
        _context.SaveChanges();
        return RedirectToAction("Profile");
    }

    [HttpGet]
    public IActionResult Librarian()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        if(isLibrarian == 0)
        {
            return BadRequest("You're not Librarian => No Access");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
