using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Data_03.Entity
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Email> EmailAddresses { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
