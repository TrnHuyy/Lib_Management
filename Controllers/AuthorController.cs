using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.IO;

namespace Lib2.Controllers;

public class AuthorController : Controller
{
    private readonly LibraryContext _context;
    public AuthorController(LibraryContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult SearchAuthor()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ViewAuthor([FromForm]string searchName)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Name == searchName);
        string content = System.IO.File.ReadAllText(author.Path);
        ViewBag.txtcontent = content;
        return View(author);
    }

    // Hiển thị form sửa tác giả
    // [HttpGet]
    // public IActionResult EditAuthor()
    // {
    //     return View();
    // }

    // [HttpPost]
    // public IActionResult EditAuthor([FromForm]int authorId,[FromForm]string content)
    // {
    //     var author = _context.Authors.FirstOrDefault(a => a.Id == authorId);
    //     var path = author.Path;
    //     if(System.IO.File.Exists(path))
    //     {
    //         string txt = System.IO.File.ReadAllText(path);
    //         txt = content;
    //         System.IO.File.WriteAllText(path, txt);
    //         return RedirectToAction("Index", "Home");
    //     }
    //     else{
    //         return BadRequest("Author Describe Not Found");
    //     }
    // }
    [HttpPost]
    public IActionResult EditAuthor([FromForm]int authorId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        if(isLibrarian == 0)
        {
            return BadRequest("You're not Librarian => No access");
        }
        var author = _context.Authors.FirstOrDefault(b => b.Id == authorId);
        if(author == null)
        {
            return BadRequest("Author Not Found.");
        }
        ViewBag.Id = authorId;
        return View();
    }

    [HttpPost]
    public IActionResult ChangeName([FromForm]int id, [FromForm]string name)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var author = _context.Authors.FirstOrDefault(u => u.Id == id);
            author.Name = name;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    public IActionResult ChangeBirthdate([FromForm]int id, [FromForm]DateTime birthdate)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var author = _context.Authors.FirstOrDefault(u => u.Id == id);
            author.Birthdate = birthdate;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    public IActionResult ChangeDescribe([FromForm]int id, [FromForm]string describe)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var author = _context.Authors.FirstOrDefault(u => u.Id == id);
            var path = author.Path;
            if(System.IO.File.Exists(path))
            {
                string txt = System.IO.File.ReadAllText(path);
                txt = describe;
                System.IO.File.WriteAllText(path, txt);
            }
            else{
                return BadRequest("Author Describe Not Found");
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public IActionResult ViewAllAuthor()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
        if(isLibrarian == 0)
        {
            return BadRequest("You're not Librarian => No Access");
        }
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var authors = _context.Authors.ToList();
        return View(authors);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}