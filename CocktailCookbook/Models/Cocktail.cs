
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }

        public string Glassware { get; set; }

        public string Garnish { get; set; }

        public User Creator { get; set; }
    }
}
