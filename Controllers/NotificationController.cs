using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lib2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Lib2.Controllers
{
    public class NotificationController : Controller
    {
        private readonly LibraryContext _context;
        public NotificationController(LibraryContext context)
        {
            _context = context;
        }

        private List<Notification> GetNotifications()
        {
            // Đây là nơi thực hiện truy vấn từ cơ sở dữ liệu hoặc một nguồn dữ liệu khác
            var notifications = _context.Notifications.ToList();
            return notifications;
            
        }
        public IActionResult Index()
        {
            // Lấy danh sách thông báo từ database hoặc nơi lưu trữ thông báo
            var notifications = GetNotifications();
            return View(notifications);
        }

        [HttpGet]
        public IActionResult CreateNotification()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateNotification(string content)
        {
            var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
            var userId = HttpContext.Session.GetInt32("UserId");
            //Console.WriteLine("hello"+isLibrarian);
            if(userId == null)
            {
                return RedirectToAction("Login", "Resigter");
            }
            if(isLibrarian == 0)
            {
               return BadRequest("ko phai thu thu nen ko co quyen tao thong bao");
            }
            else
            {
                var newNotification = new Notification
                {
                    Content = content,
                    CreatedAt = DateTime.UtcNow // Gán thời gian hiện tại
                };
                _context.Notifications.Add(newNotification);
                _context.SaveChanges();
                return RedirectToAction("Index", "Notification"); // Redirect về trang chủ sau khi thêm thông báo
            }
        }

        [HttpPost]
        public IActionResult DeleteNotification([FromForm]int notiId)
        {
            var isLibrarian = HttpContext.Session.GetInt32("isLibrarian");
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return RedirectToAction("Login", "Resigter");
            }
            if(isLibrarian == 0)
            {
                return BadRequest("ko phai thu thu");
            }
            else{
                var noti = _context.Notifications.FirstOrDefault(n => n.Id == notiId);
                _context.Notifications.Remove(noti);
                _context.SaveChanges();
                return RedirectToAction("Index", "Notification");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}