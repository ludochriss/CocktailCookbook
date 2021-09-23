using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
        public string Cocktail { get; set; }

    }
}
