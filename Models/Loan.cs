using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib2.Models;

namespace Lib2.Models;
public class Loan
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int BookId { get; set; }

    // DueDate: ngày sách mượn phải được trả
    // ScheduleLoanDate: ngày đến cửa hàng mượn thực tế
    // LoanDate :Ngày đặt mượn trên web
    // ReturnDate: ngày trả thực tế - null nếu chưa trả
    // ScheduleReturnDate: ngày đặt trả trên web
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public DateTime ScheduleLoanDate {get; set;}
    public DateTime ScheduleReturnDate {get; set;}
    public User User { get; set; }
    public Book Book { get; set; }
    
    //khởi tạo trạng thái mượn sách
    public Loan()
    {
        LoanDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(14); // Mỗi cuốn sách được mượn trong 14 ngày
        ReturnDate = null;
    }

    public Loan(int? userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        LoanDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(14); // Mỗi cuốn sách được mượn trong 14 ngày
        ReturnDate = null;
        //User = _context.Users.FirstOrDefault(l => l.Id == userId);
        //Book = _context.Books.FirstOrDefault(b => b.Id == bookId);
    }
}
