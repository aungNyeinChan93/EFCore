using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class TeacherUser
    {
        public int TeacherId { get; set; }

        [JsonIgnore]
        public Teacher? Teacher { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
