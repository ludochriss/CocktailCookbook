using CocktailCookbook.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
        public string Cocktail { get; set; }
              

        public string ReturnImageAsFileStream(CreateCocktailViewModel model, IWebHostEnvironment _webHostEnvironment)
        {
            string uniqueFileName = null;

           
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            
            return uniqueFileName;
        }
    
    }
   
}
