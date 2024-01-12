using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lib2.Models
{
    public class Favorite
    {
        public int UserId { get; set;}
        public int BookId { get; set; }
        public User user {get; set;}
        public Book book {get; set;}
        public Favorite(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}