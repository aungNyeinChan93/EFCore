using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore.Data_03.Entity
{
    public class Email
    {
        public int EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        //public int PersonId { get; set; }
        //public Person Person { get; set; }
    }
}
