using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class CategoryPost
    {
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public int PostId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }


    }
}
