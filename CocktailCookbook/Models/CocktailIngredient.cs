using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{

   //is the normalisation table for the list of ingredients that need to go in each cocktail

    public class CocktailIngredient
    {
       
        public int CocktailId { get; set; }
        public string IngredientId { get; set; }

        public string  Quantity { get; set; }

        

    }
}
