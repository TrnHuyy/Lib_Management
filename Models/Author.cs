using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Lib2.Models
{
    public class Author
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime Birthdate {get; set;}
        public string Path {get; set;}
    }
}