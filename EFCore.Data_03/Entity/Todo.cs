using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Data_03.Entity
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }

        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
    }
}
