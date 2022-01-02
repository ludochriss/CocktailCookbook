using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace CocktailCookbook.Models
{
    public class ReplyCommentViewModel
    {
        public string Id { get; set; }

        [Display(Name = "comment by")]

        public User Author { get; set; }
        public DateTime Time { get; set; }
        [Required]

        public string Content { get; set; }

        public string Title { get; set; }
        public Post OriginalPost { get; set; }
       
    }
}
