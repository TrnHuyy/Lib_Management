using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Lib2.Models
{
    public class Librarian
    {
        [Key]
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public int IsLibrarian {get; set;}
    
        // Danh sách các sách mà người dùng đã mượn
        public ICollection<Loan> Loans { get; set; }

        // Constructor để khởi tạo danh sách mượn
        // public User()
        // {
        //     Loans = new List<Loan>();
        // }
        public Librarian()
        {
            Id = 0;
            Password = "";
            Email = "";
        }
        public Librarian( string email, string pass)
        {
            Password = pass;
            Email = email;
        }
 
    }
}