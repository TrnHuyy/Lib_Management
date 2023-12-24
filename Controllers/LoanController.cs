using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace Lib2.Controllers;

public class LoanController : Controller
{
    private readonly LibraryContext _context;
    public LoanController(LibraryContext context)
    {
        _context = context;
    }

    // Hiển thị danh sách các sách đã mượn.
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        // Lấy thông tin lịch sử mượn sách từ cơ sở dữ liệu dựa trên userId
        if(userId != null)
        {
            var loanHistory = _context.Loans
                                .Include(l => l.Book) // Include để lấy thông tin sách
                                .Include(l => l.User)
                                .Where(l => l.UserId == userId)
                                .ToList();
            return View(loanHistory);
        }
        else
        {
            return RedirectToAction("Login", "Resigter");
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create([FromForm]Loan loan)
    {
        if (ModelState.IsValid)
        {
            // Lưu thông tin mượn sách vào cơ sở dữ liệu
            _context.Loans.Add(loan);
            loan.Book.Borrow();
            _context.SaveChanges();
            return RedirectToAction("Index", "Loan"); // Chuyển hướng đến action Index của LoanController sau khi tạo mượn sách thành công
        }

        return View(loan);
    }

    [HttpPost]
    public IActionResult CreateLoan([FromForm]int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if(userId != null)
        {
            var loan = new Loan(userId, bookId);
            _context.Loans.Add(loan);
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            book.Borrow();
            _context.SaveChanges();
            return RedirectToAction("Index", "Loan");
        }
        else
        {
            return RedirectToAction("Login", "Resigter");
        }
    }

    [HttpPost]
    public IActionResult UpdateReturnDate([FromForm]int loanId)
    {
        var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);
        if(loan == null)
        {
            return NotFound();
        }        
        if(loan.ReturnDate != null)
        {
            return BadRequest("sach da duoc tra");
        }
        loan.ReturnDate = DateTime.Now;
        loan.Book.UnBorrow();
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteLoan([FromForm]int loanId)
    {
        var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);
        if(loan == null)
        {
            return NotFound();
        }   
        _context.Loans.Remove(loan);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
