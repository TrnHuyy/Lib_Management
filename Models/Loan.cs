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
    // LoanDate: ngày mượn
    // ReturnDate: ngày trả thực tế - null nếu chưa trả
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
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
    }
}
