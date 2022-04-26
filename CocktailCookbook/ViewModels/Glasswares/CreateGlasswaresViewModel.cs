using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CocktailCookbook.ViewModels.Glasswares
{
    public class CreateGlasswaresViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        
        [Display(Name="Photo of Glassware")]

        public IFormFile Photo { get; set; }


    }
}

