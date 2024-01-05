using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace Lib2.Controllers;

public class BookController : Controller
{
    private readonly LibraryContext _context;
    public BookController(LibraryContext context)
    {
        _context = context;
    }

    // Index hiển thị thanh tìm kiếm sách
    public IActionResult Index()
    {
        return View();
    }

    // Form điền tên sách
    public IActionResult Search([FromForm]string searchName)
    {
        if (!string.IsNullOrEmpty(searchName))
        {
            var results = _context.Books
                .Where(b => b.Title.Contains(searchName))
                .ToList();
            foreach(var kq in results)
            {
                Console.WriteLine(kq.Title);
            }
            
            return View("SearchResults", results);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ReadBook([FromForm]int BookId)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == BookId);
        Console.WriteLine(BookId);
        var filePath = book.Path;
        if(filePath == null)
        {
            return BadRequest("ko co noi dung");
        }
        if(System.IO.File.Exists(filePath))
        {
            string content = System.IO.File.ReadAllText(filePath);
            return Content(content);
        }
        else{
            return BadRequest("ko tim thay noi dung");
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
