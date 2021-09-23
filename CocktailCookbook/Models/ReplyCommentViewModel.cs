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

        public string Author { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public string Content { get; set; }
        public int PostId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
