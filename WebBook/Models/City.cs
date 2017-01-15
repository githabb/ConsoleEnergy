using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WebBook.Models
{
    public class City
    {
        

        public int CodCity { get; set; }

        [Required]
        public string Name { get; set; }
    }
}