using CocktailCookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class CreateTaskViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User Creator { get; set; }

        public DateTime TimeCreated { get; set; }

        public string Department { get; set; }
        public string TaskDescription { get; set; }

    }
}
