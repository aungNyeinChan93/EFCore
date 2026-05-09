using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.domain.Entities
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }

        public required string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
