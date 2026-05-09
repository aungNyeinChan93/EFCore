using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.domain.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public required string name { get; set; }

        public ICollection<TeacherUser> TeacherUser { get; set; }
    }
}
