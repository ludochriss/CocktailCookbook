
using CocktailCookbook.Interfaces;
using CocktailCookbook.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

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
        public Glassware Glassware { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Garnish")]
        public string Garnish { get; set; }

        [Display(Name = "Cocktail by ")]
        public User Creator { get; set; }

      
        public string Photo { get; set; }


        //this method could be placed into a services layer that could apply to multiple items 
        public string ConvertImageToFilePath(IFormFile photo,IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
