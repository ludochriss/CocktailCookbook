using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class Authorisation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAuthorised { get; set; }

    }
}
