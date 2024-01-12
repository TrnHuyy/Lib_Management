using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Lib2.Controllers;

public class BookController : Controller
{
    private readonly LibraryContext _context;
    public BookController(LibraryContext context)
    {
        _context = context;
    }

    // Hiển thị thông tin chi tiết của sách
    [HttpPost]
    public IActionResult Details([FromForm]int bookId)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if(book == null)
        {
            return BadRequest("Not Found");
        }
        var comments = _context.Comments.Where(c => c.BookId == bookId).ToList();
        var viewModel =  new BookDetailsViewModel(book, comments);
        return View(viewModel);
    }

    // Hiển thị thanh tìm kiếm sách
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
            return BadRequest("Selected book has no content");
        }
        if(System.IO.File.Exists(filePath))
        {
            string content = System.IO.File.ReadAllText(filePath);
            return Content(content);
        }
        else{
            return BadRequest("Book Not Found");
        }
    }

    [HttpPost]
    public IActionResult AddFavoriteBook([FromForm]int BookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        var book = _context.Books.FirstOrDefault(b => b.Id == BookId);
        Console.WriteLine(userId);
        Console.WriteLine(user.Id);
        if(book == null)
        {
            return BadRequest("Book Not Found");
        }
        bool check = _context.Favorites.Any(f => f.UserId == userId && f.BookId == BookId);
        if(check == false)
        {
            var fav = new Favorite(user.Id, BookId);
            _context.Favorites.Add(fav);
            _context.SaveChanges();
            return RedirectToAction("ViewFavoriteBook", "Book");
        }
        else{
            return BadRequest("Selected book already in Favorites");
        }
    }
    
    public IActionResult ViewFavoriteBook()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var booksid = _context.Favorites.Where(f => f.UserId == userId).Select(f => f.BookId).ToList();
        var books = _context.Books.Where(b => booksid.Contains(b.Id)).ToList();
        return View(books);
    }

    [HttpPost]
    public IActionResult DeleteFavoriteBook([FromForm]int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var favorite = _context.Favorites.FirstOrDefault(f => f.UserId == userId && f.BookId == bookId);
        if(favorite == null)
        {
            return BadRequest("Favorite book Not Found");
        }
        _context.Favorites.Remove(favorite);
        _context.SaveChanges();
        return RedirectToAction("ViewFavoriteBook", "Book");
    }

    // Hiển thị form sửa sách
    [HttpPost]
    public IActionResult Edit([FromForm]int bookId)
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
        var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if(book == null)
        {
            return BadRequest("Book Not Found.");
        }
        ViewBag.Id = bookId;
        return View();
    }

    // Sua sach
    // [HttpPost]
    // public IActionResult EditBook([FromForm]int id,[FromForm]string title, [FromForm]string author, [FromForm]string category)
    // {
    //     var book = _context.Books.FirstOrDefault(b => b.Id == id);
    //     if(book == null)
    //     {
    //         return NotFound();
    //     }
    //     book.Title = title;
    //     book.Author = author;
    //     book.Category = category;
    //     _context.SaveChanges();
    //     return RedirectToAction("Index", "Home");
    // }

    [HttpPost]
    public IActionResult ChangeTitle([FromForm]int id, [FromForm]string title)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var book = _context.Books.FirstOrDefault(u => u.Id == id);
            book.Title = title;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public IActionResult ChangeAuthor([FromForm]int id, [FromForm]string author)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var book = _context.Books.FirstOrDefault(u => u.Id == id);
            book.Author = author;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public IActionResult ChangeCategory([FromForm]int id, [FromForm]string category)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        else{
            var book = _context.Books.FirstOrDefault(u => u.Id == id);
            book.Category = category;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public IActionResult AddComments([FromForm]int bookId, [FromForm]string content)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        var comment = new Comment {
            UserId = userId,
            BookId = bookId,
            Date = DateTime.Now
        };
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
