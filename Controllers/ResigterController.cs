using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Lib2.Controllers;

public class ResigterController : Controller
{
    public class AddUserDto
    {
        //public int id {get; set;}
        public string name {get; set;}
        public string email {get; set;}
        public string password {get; set;}
    }
    private readonly LibraryContext _context;
    public ResigterController(LibraryContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index([FromForm]AddUserDto dto)
    {
        //Console.WriteLine(dto.id+dto.name+dto.email+dto.password);
        // var users = _context.Users.ToList();
        // int id = users.Max(user => user.Id);
        var user = new User(dto.name, dto.email, dto.password);
        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
