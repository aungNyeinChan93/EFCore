using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class UserDetail
    {
        [Key]
        public int UserDetailId { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
