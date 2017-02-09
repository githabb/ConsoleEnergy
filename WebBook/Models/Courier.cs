using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBook.Models
{
    public class Courier
    {
        
        public int CodCourier { get; set; }

        [Required]
        public string Surname { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Otchectvo { get; set; }

        public string Gender { get; set; }

        [Required]
        public int? CodCity { get; set; }

        public DateTime? Datebirth { get; set; }
    }
}