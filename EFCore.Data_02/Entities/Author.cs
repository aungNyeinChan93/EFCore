using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data_02.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }

        public required string Name { get; set; }

        public AuthorDetail AuthorDetail { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
}
