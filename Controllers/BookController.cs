using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;

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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
