using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        [JsonIgnore]
        public UserDetail? UserDetail { get; set; }

        [ForeignKey("SchoolId")]
        public int SchoolId { get; set; }

        [JsonIgnore]
        public School? School { get; set; }

        public ICollection<TeacherUser> TeacherUser { get; set; }
    }
}
