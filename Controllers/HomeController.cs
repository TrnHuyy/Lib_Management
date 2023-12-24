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
        var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
        if(isLibrarian == 1)
        {
        var users = _context.Users.ToList();
        return View(users);
        }
        else
        {
            return RedirectToAction("Error");
        }
    }

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
            return BadRequest("ko tim thay nguoi dung");
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
            return RedirectToAction("Profile");
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
            return BadRequest("Sai mat khau cu");
        }
        user.ChangePass(newPass);
        _context.SaveChanges();
        return RedirectToAction("Profile");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
