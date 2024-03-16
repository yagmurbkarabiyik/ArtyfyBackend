using ArtyfyBackend.Core.Models.Category;
using ArtyfyBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtyfyBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _categoryService.GetAllAsync());
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCategory([FromQuery] CategoryModel model)
        {
            return CreateActionResult(await _categoryService.AddAsync(model));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel model)
        {
            return CreateActionResult(await _categoryService.UpdateAsync(model, id));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return CreateActionResult(await _categoryService.RemoveAsync(id));
        }
    }
}