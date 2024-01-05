using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

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

    // [HttpPost]
    // public ActionResult Create([FromForm]Loan loan)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         // Lưu thông tin mượn sách vào cơ sở dữ liệu
    //         _context.Loans.Add(loan);
    //         loan.Book.Borrow();
    //         _context.SaveChanges();
    //         return RedirectToAction("Index", "Loan"); // Chuyển hướng đến action Index của LoanController sau khi tạo mượn sách thành công
    //     }

    //     return View(loan);
    // }

    [HttpPost]
    public IActionResult CreateLoan([FromForm]int bookId)
    {
        Console.WriteLine("new"+bookId);
        var userId = HttpContext.Session.GetInt32("UserId");
        var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        if(book.IsBorrowed == 1)
        {
            return BadRequest("sach da duoc muon");
        }
        else
        {
            var loan = new Loan(userId, bookId);
            _context.Loans.Add(loan);
            var book1 = _context.Books.FirstOrDefault(b => b.Id == bookId);
            book1.Borrow();
            _context.SaveChanges();
            return RedirectToAction("Index", "Loan");
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
        if(loan.ReturnDate == null && loan != null)
        {
            loan.ReturnDate = DateTime.Now;
            var book = _context.Books.FirstOrDefault(b => b.Id == loan.BookId);
            book.UnBorrow();
            _context.SaveChanges();
            return RedirectToAction("ViewAllLoan");
        }
        else{
            return BadRequest("sach da duoc tra");
        }
    }

    [HttpPost]
    public IActionResult DeleteLoan([FromForm]int loanId)
    {
        var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);
        if(loan == null)
        {
            return NotFound();
        }
        //loan.Book.UnBorrow();
        _context.Loans.Remove(loan);
        _context.SaveChanges();

        return RedirectToAction("ViewAllLoan");
    }

    [HttpGet]
    public IActionResult ViewAllLoan()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
        if(userId == null)
        {
            return RedirectToAction("Login", "Resigter");
        }
        if(isLibrarian == 0)
        {
            return BadRequest("ko phai thu thu");
        }
        else{
            var loans = _context.Loans.ToList();
            //var viewModel = new ViewAllLoanViewModel(loans);
            return View(loans);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
