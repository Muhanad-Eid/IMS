using Application.Services.ProductService;
using Application.Services.ProductService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<List<GetProductDto>>> GetProducts()
        {
            var data = await _productService.GetProducts();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
            var data = await _productService.GetProduct(id);
            return Ok(data);    
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto item)
        {
            await _productService.AddProduct(item);
            return Ok();
        }
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id,AddProductDto item)
        {
            await _productService.UpdateProduct(id,item);
            return Ok();
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }

    }
}
