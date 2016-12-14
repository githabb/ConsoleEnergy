using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBook.Models
{
    public class Book
    {
       
        [Required]
        // название книги
        public string Name { get; set; }

        // автор книги
        public string Author { get; set; }

        [Required]
        // цена книги
        public double Price { get; set; }

        // код книги
        public int CodBook { get; set; }
    }
}