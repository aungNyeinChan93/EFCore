using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Data_02.Entities
{
    public class AuthorDetail
    {
        [Key]
        public int AuthorDetailId { get; set; }

        public required string Bio { get; set; }

        public int Age { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
