using EFCore.Data_02.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.domain.Models.Categories
{
    public class CategoryResponseModel
    {
        public List<CategoryDto> Categories { get; set; }
    }

    public class CategoryDto
    {

        public int CategoryId { get; set; }

        public required string Name { get; set; }

        public ICollection<CategoryPost>? CategoryPosts { get; set; }
    }

    public class CreateCategoryDto
    {
        [Required]
        public required string Name { get; set; }

        public ICollection<CategoryPost>? CategoryPosts { get; set; } = null!;
    }
}
