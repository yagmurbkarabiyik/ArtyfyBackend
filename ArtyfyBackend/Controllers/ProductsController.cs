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

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel model)
        {
            return CreateActionResult(await _productService.Create(model));
        }

        /// <summary>
        /// This method listed sellable products to show our market.
        /// </summary>
        [HttpGet("getSellableProducts")]
        public async Task<IActionResult> GetSellableProducts()
        {
            return CreateActionResult(await _productService.ListSellableProduct());
        }
    }
}