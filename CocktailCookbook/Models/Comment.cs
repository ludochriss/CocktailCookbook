using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public User Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public int  PostId { get; set; }
    }
}
