using CocktailCookbook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CocktailCookbook.ViewModels
{
    public class CreateCocktailViewModel
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
        
        [Display(Name ="Select Glassware")]
        public string Glassware { get; set; }

       
        public string Description { get; set; }
        
        public string Garnish { get; set; }

        [Display(Name = "Cocktail by ")]
        public User Creator { get; set; }

        
        public IFormFile Photo { get; set; }

        public List<SelectListItem> PopulateGlasswareSelectList(List<Glassware> glasses)
        {

           var  selectList = new List<SelectListItem>();
            if (glasses.Count > 0)
            {
                foreach (var glass in glasses)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = glass.Id.ToString(),
                        Text = glass.Name
                    });
                }
                return selectList;
            }
            else
            {
                selectList.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "Please create glassware first"
                });
                return selectList;
            }
        }
    }
}
