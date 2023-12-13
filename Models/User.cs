using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Lib2.Models
{
    public class User
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLibrarian {get; set;}
        //public string Email { get; set; }
    
        // Danh sách các sách mà người dùng đã mượn
        //public virtual ICollection<Loan> Loans { get; set; }

        // Constructor để khởi tạo danh sách mượn
        // public User()
        // {
        //     Loans = new List<Loan>();
        // }
        public User()
        {
            Id = 0;
            Name = "";
            Password = "";
            Email = "";
            IsLibrarian = false;
        }
        public User(int id, string name, string email, string pass)
        {
            Id = id;
            Name = name;
            Password = pass;
            Email = email;
            IsLibrarian = false;
        }
        public User( string name, string email, string pass)
        {
            Name = name;
            Password = pass;
            Email = email;
            IsLibrarian = false;
        }
    }
}