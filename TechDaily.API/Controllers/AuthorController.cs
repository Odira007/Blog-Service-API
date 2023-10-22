using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechDaily.Apllication.Interfaces;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AuthorResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorRequestDto request)
        {
            var response = await _authorService.AddAsync(request);
            if(response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("all")]
        public IActionResult GetAllAuthors(int pageSize, int pageNumber)
        {
            var response = _authorService.GetAllAuthors(pageSize, pageNumber);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }
    }
}
