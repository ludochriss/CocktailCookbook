using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    [NotMapped]
    public class Venue
    {
        public string Id { get; set; }

        public string Name { get; set; }
                
        public List<Department> Departments { get; set; }              

        public List<User> Staff { get; set; }

        public List<Cocktail> Cocktails { get; set; }



    }
}
