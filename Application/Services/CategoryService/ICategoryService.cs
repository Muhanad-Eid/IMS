using Application.Services.CategoryService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<GetCategoryDto>> GetCategories();
        Task<GetCategoryDto> GetCategory(int Id);
        Task CreateCategory(CreateCategoryDto category);
        Task DeleteCategory(int id);
        Task UpdateCategory(int id,UpdateCategoryDto category);
    }
}
