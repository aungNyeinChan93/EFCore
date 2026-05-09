using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data_02.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }

        public ICollection<CategoryPost> CategoryPosts { get; set; }
    }
}
