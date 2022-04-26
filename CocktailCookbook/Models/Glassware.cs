using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CocktailCookbook.Models
{
    public class Glassware
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public string ImagePath { get; set; }

        public string ConvertImageToFilePath(IFormFile photo, IWebHostEnvironment webHostEnvironment)
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
