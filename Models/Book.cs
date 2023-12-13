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
        public bool IsBorrowed { get; set; }
        
        [NotMapped]
        public ICollection<Loan> Loans {get; set; }

        // Danh sách các mượn sách của cuốn sách
        // public virtual ICollection<Loan> Loans { get; set; }

        // Các thuộc tính khác của cuốn sách

        // Constructor để khởi tạo danh sách mượn
        // public Book()
        // {
        //     Loans = new List<Loan>();
        // }
        public Book()
        {
            Title = "";
            Author = "";
            Category = "";
            IsBorrowed = false;
        }
        public Book(string title, string author, string category, bool isborrowed, string path)
        {
            Title = title;
            Author = author;
            Category = category;
            IsBorrowed = isborrowed;
            Path = path;
        }
    }
}