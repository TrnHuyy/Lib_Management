using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lib2.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set;}
        public string Path { get; set; }
        public int IsBorrowed { get; set; }
        public ICollection<Loan> Loans {get; set; }

        // Danh sách các mượn sách của cuốn sách
        // public virtual ICollection<Loan> Loans { get; set; }

        // Các thuộc tính khác của cuốn sách

        // Constructor để khởi tạo danh sách mượn
        // public Book()
        // {
        //     Loans = new List<Loan>();
        // }
        public void Borrow()
        {
            IsBorrowed = 1;
        }
        public void UnBorrow()
        {
            IsBorrowed = 0;
        }
        public Book()
        {
            Title = "";
            Author = "";
            Category = "";
            IsBorrowed = 0;
        }
        public Book(string title, string author, string category, int isborrowed, string path)
        {
            Title = title;
            Author = author;
            Category = category;
            IsBorrowed = isborrowed;
            Path = path;
        }
    }
}