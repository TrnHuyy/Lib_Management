using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Lib2.Controllers;

public class DebtController : Controller
{
    private readonly LibraryContext _context;
    public DebtController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult UpdateDebt()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UpdateDebt([FromForm]int userid, [FromForm]long debt)
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
        var user = _context.Users.FirstOrDefault(u => u.Id == userid);
        if(user == null)
        {
            return BadRequest("User Not Found");
        }
        user.Debt = debt;
        _context.SaveChanges();
        return RedirectToAction("ViewAllDebt", "Debt");
    }

    [HttpGet]
    public IActionResult ViewAllDebt()
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
        else{
            var users = _context.Users.ToList();
            return View(users);
        }
    }

    // [HttpGet]
    // public IActionResult PayDebt()
    // {
    //     return View();
    // }

    // [HttpPost]
    // public IActionResult PayDebt(PayDebtViewModel viewModel)
    // {
    //     var userId = HttpContext.Session.GetInt32("UserId");
    //     var user = _context.Users.FirstOrDefault(u => u.Id == userId);
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}