using Application.Services.CategoryService;
using Application.Services.CategoryService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var data =await _categoryService.GetCategories();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var data = await _categoryService.GetCategory(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto category)
        {
            await _categoryService.CreateCategory(category);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto category)
        {
            await _categoryService.UpdateCategory(id, category);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}