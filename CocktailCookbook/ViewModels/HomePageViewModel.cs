using CocktailCookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class HomePageViewModel
    {
        public List<Models.Task> Tasks { get; set; }

        public List<CompletedTask> CompletedTasks { get; set; }
        public List<Cocktail> Cocktails { get; set; }

    }
}
