using Microsoft.AspNetCore.Mvc;
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
        public string Content { get; set; }
        public string Author { get; set; }

        public DateTime Time { get; set; }
        [Required]

       

        
       
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        [Display(Name = "comment by")]
        public string PostAuthor { get; set; }
        public string AuthorUserId { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
