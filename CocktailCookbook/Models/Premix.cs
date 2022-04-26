using System.ComponentModel.DataAnnotations;

namespace CocktailCookbook.Models
{
    public class Premix
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name="Par Weekday")]
        public string ParWeekday { get; set; }

        [Display(Name = "Par Weekend")]
        public string ParWeekend { get; set; }

        public string Ingredients { get; set; }

        [Display(Name="Instructions/ Method")]
        public string Method { get; set; }

    }
}
