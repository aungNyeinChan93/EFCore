using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data_02.Entities
{
    public class CategoryPost
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }



        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
