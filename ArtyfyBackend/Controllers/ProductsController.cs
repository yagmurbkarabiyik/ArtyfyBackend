using ArtyfyBackend.Bll.Services;
using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtyfyBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromQuery] ProductModel model)
        {
            return CreateActionResult(await _productService.AddAsync(model));   
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromQuery] ProductModel model, int id)
        {
            return CreateActionResult(await _productService.UpdateAsync(model,id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return CreateActionResult(await _productService.RemoveAsync(id));
        }
    }
}