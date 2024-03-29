﻿using CocktailCookbook.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CocktailCookbook.ViewModels
{
    public class EditCocktailViewModel
    {
        public int Id { get; set; }

        //cocktails need a list of ingredients that are contained in a CocktailIngredient Table that 
        [Required]
        public string Name { get; set; }
        [Display(Name = "Method")]
        [Required]
        public string Method { get; set; }
        [Display(Name = "Ingredients")]
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public string Glassware { get; set; }

        public string Description { get; set; }

        public string Garnish { get; set; }

        [Display(Name = "Cocktail by ")]
        public User Creator { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Photo { get; set; }
    }
}
