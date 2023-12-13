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
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var loans = await _context.Loans.ToListAsync();
        return View(loans);
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
            _context.SaveChanges();
            return RedirectToAction("Index", "Loan"); // Chuyển hướng đến action Index của LoanController sau khi tạo mượn sách thành công
        }

        return View(loan);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
