using EFCore.Data_02.Data;
using EFCore.Data_02.Entities;
using EFCore.domain.Models.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.domain.Services
{
    public class CategoryService
    {
        private readonly AppDbContext2 _context;

        public CategoryService()
        {
            _context = new AppDbContext2();
        }

        public async Task<CategoryResponseModel> GetAllAsync()
        {
            var categories = await _context.Categories.AsNoTracking()
                .Select(x => new CategoryDto { CategoryId = x.CategoryId, Name = x.Name, CategoryPosts = x.CategoryPosts })
                .ToListAsync();

            var responseModle = new CategoryResponseModel()
            {
                Categories = categories
            };

            return responseModle;
        }


        public async Task<Category?> CreateAsync(CreateCategoryDto createCategoryDto)
        {

            var newCategory = new Category
            {
                Name = createCategoryDto.Name,
            };

            if (createCategoryDto.CategoryPosts is not null )
            {
                newCategory.CategoryPosts = createCategoryDto.CategoryPosts;
            }

            await _context.Categories.AddAsync(newCategory);
            var result = await _context.SaveChangesAsync();
            return result >= 1 ? newCategory : default!;
        }

    }
}
