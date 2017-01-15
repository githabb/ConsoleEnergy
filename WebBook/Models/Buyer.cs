using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WebBook.Models
{
    public class Buyer
    {




        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string PochtovuyAdress { get; set; }

        [Required]
        public long CodBuyer { get;  set; }
        
        public string Name { get;  set; }
       
        public string Otchectvo { get; set; }
        
        public string Floor { get; set; }
       
        public string Email { get; set; }

        public int? CityId { get; set; }

    }
   
}

       
