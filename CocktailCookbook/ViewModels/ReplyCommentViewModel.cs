using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class ReplyCommentViewModel

    {
        public string Id { get; set; }

        [Display(Name = "comment by")]
        public string Author { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public List<Models.Comment> Comments { get; set; }
    }
}
