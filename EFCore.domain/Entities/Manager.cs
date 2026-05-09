using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore.domain.Entities
{
    [Table("Tbl_Managers")]
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }

        [Column("ManagerName")]
        public required string Name { get; set; }
    }
}
