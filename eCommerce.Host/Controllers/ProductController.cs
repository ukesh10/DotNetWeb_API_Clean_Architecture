using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllAsync();
            return products.Any() ? Ok(products) : NotFound(products);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var product = await productService.GetByIdAsync(id);
            return product != null ? Ok(product) : NotFound(product);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await productService.AddAsync(product);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await productService.UpdateAsync(product);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await productService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
