using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryService.GetAllAsync();
            return categories.Any() ? Ok(categories) : NotFound(categories);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var category = await categoryService.GetByIdAsync(id);
            return category != null ? Ok(category) : NotFound(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await categoryService.AddAsync(category);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await categoryService.UpdateAsync(category);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await categoryService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
