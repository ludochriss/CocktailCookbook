using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
       
        public string Email { get; set; }


        [NotMapped]
        public Availability Availability { get; set; }


    }
}
