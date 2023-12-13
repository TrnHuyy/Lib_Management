using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib2.Models;

namespace Lib2.Models;
public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }

    // DueDate: ngày sách mượn phải được trả
    // LoanDate: ngày mượn
    // ReturnDate: ngày trả thực tế - null nếu chưa trả
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    private readonly LibraryContext _context;

    public Loan(){
        
    }
    //khởi tạo trạng thái mượn sách
    public Loan(LibraryContext context)
    {
        _context = context;
        LoanDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(14); // Mỗi cuốn sách được mượn trong 14 ngày
        ReturnDate = null;
    }
}
