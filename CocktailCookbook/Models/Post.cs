using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    
    public class Post
    {

        public int Id { get; set; }
        [Display(Name ="Post Subject")]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime),Display(Name ="Posted at : ")]
        
        public DateTime TimeCreated { get; set; }
        [Display(Name ="posted by : ")]
        public User Author { get; set; }

       
        public List<Comment> Comments { get; set; }

    }
}
