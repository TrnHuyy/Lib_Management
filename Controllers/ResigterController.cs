using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lib2.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


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
    private readonly IHttpContextAccessor _httpContext;
    public ResigterController(LibraryContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Tìm người dùng từ cơ sở dữ liệu dựa trên email
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        var lib = _context.Librarians.FirstOrDefault(u => u.Email == email);
        int check = 0;
        if(lib != null)
            check = 1;

        if (user != null)
        {
            if (user.Password == password)
            {
                // Đăng nhập thành công, thực hiện hành động mong muốn
                _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);
                _httpContext.HttpContext.Session.SetInt32("isLibrarian", check);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Mật khẩu không đúng, hiển thị thông báo lỗi
                ViewBag.ErrorMessage = "Invalid password.";
                return View();
            }
        }
        else
        {
            // Người dùng không tồn tại, hiển thị thông báo lỗi
            ViewBag.ErrorMessage = "User not found.";
            return View();
        }
    }

    [HttpGet]
    public IActionResult Resign()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Resign([FromForm]AddUserDto dto)
    {
        //Console.WriteLine(dto.id+dto.name+dto.email+dto.password);
        // var users = _context.Users.ToList();
        // int id = users.Max(user => user.Id);
        var user = new User(dto.name, dto.email, dto.password);
        _context.Users.Add(user);   
        _context.SaveChanges();
        _httpContext.HttpContext.Session.SetInt32("UserId", user.Id);
        //_httpContext.HttpContext.Session.SetInt32("isLibrarian");
        return RedirectToAction("Index", "Home");
    }

    public IActionResult LogOut()
    {
        _httpContext.HttpContext.Session.Remove("UserId");
        _httpContext.HttpContext.Session.Remove("isLibrarian");
        return RedirectToAction("Login", "Resigter");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
