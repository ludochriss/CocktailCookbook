using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailCookbook.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public User Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public int  PostId { get; set; }
    }
}
