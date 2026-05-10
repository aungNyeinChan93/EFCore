using EFCore.Data_02.Entities;
using EFCore.domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.domain.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                CategoryPosts = category.CategoryPosts
            };
        }
    }
}
