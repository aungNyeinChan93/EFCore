using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public required string Title { get; set; }

        public ICollection<CategoryPost> CategoryPost { get; set; }
    }
}
