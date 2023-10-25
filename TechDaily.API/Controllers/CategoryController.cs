using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechDaily.Apllication.Interfaces;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CategoryResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto request)
        {
            var response = await _categoryService.AddAsync(request);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CategoryResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult GetAllCategories(int pageSize, int pageNumber)
        {
            var response = _categoryService.GetAllCategories(pageSize, pageNumber);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetById(id);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCategory(string id, CategoryRequestDto request)
        {
            await _categoryService.UpdateCategory(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}
