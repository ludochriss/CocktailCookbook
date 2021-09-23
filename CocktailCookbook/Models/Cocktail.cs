
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailCookbook.Models
{
    public class Cocktail
    {
        public int Id { get; set; }

        //cocktails need a list of ingredients that are contained in a CocktailIngredient Table that 
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name="Method")]
        public string Method { get; set; }
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }

        [Display(Name = "Glassware")]
        public string Glassware { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Garnish")]
        public string Garnish { get; set; }

        [Display(Name = "Cocktail by ")]
        public User Creator { get; set; }

       
        public string Photo { get; set; }

    }
}
