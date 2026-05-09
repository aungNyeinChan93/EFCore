using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public required string Name { get; set; }

        public required string Content { get; set; }


        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public ICollection<CategoryPost> CategoryPost { get; set; }


    }
}
