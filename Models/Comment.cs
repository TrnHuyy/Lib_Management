using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lib2.Models
{
    public class Comment 
    {
        [Key]
        public int Id { get; set;}
        public int BookId { get; set; }
        public int? UserId {get; set; }
        public DateTime Date {get; set;}
        public string Content {get; set;}
    }
}