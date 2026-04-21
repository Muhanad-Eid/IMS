using Application.Repositories;
using Application.Services.CategoryService.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CategoryService
{
    public class CategoryService: ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategory(CreateCategoryDto category)
        {
            var existingCategory = await _categoryRepository.GetAll().AnyAsync(u => u.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if(existingCategory)
            {
                throw new Exception("A category with the same name already exists.");
            }
            var data = new Category
            {
                Name = category.Name,
                Description = category.Description,
                Sort = category.Sort
            };
            await _categoryRepository.CreateAsync(data);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetAll().Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

            if (category.Products.Any())
            {
                throw new Exception("There products under this category");
            }
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<List<GetCategoryDto>> GetCategories()
        {
            var data = await _categoryRepository.GetAll().Select(x => new GetCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Sort = x.Sort
            }).OrderBy(x => x.Sort).ToListAsync();

            return data;

        }

        public async Task<GetCategoryDto> GetCategory(int id)
        {
            var data=  await _categoryRepository.GetByIdAsync(id);
            var user = new GetCategoryDto
            {
                Id = data.Id,
                Name=data.Name,
                Description=data.Description,
                Sort=data.Sort
            };
            return user;
        }

        public async Task UpdateCategory(int id, UpdateCategoryDto category  )
        {
            var existingCategory = await _categoryRepository.GetAll().AnyAsync(u => u.Name.ToLower().Trim() == category.Name.ToLower().Trim()&& u.Id!=id);
            if (existingCategory)
            {
                throw new Exception("A category with the same name already exists.");
            }
            var data= await _categoryRepository.GetByIdAsync(id);
            data.Name = category.Name;
            data.Description = category.Description;
            data.Sort = category.Sort;
            _categoryRepository.Update(data);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
