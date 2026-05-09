using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Data_02.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<CategoryPost> CategoryPosts { get; set; }
    }
}
