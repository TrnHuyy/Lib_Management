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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Debt {get; set;}
        //public int IsLibrarian {get; set;}
    
        // Danh sách các sách mà người dùng đã mượn
        public ICollection<Loan> Loans { get; set; }
        public ICollection<Favorite> Favorites {get; set;}

        public void ChangeName(string name)
        {
            Name =name;
        }
        public void ChangePass(string pass)
        {
            Password = pass;
        }
        public User()
        {
            Id = 0;
            Name = "";
            Password = "";
            Email = "";
            //IsLibrarian = 0;
        }
        public User(string name, string email, string pass)
        {
            Name = name;
            Password = pass;
            Email = email;
            //IsLibrarian = 0;
        }
        public User( string name, string email, string pass, int isLibrarian)
        {
            Name = name;
            Password = pass;
            Email = email;
            //IsLibrarian = isLibrarian;
        }
    }
}