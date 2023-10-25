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
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ArticleResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> AddArticle([FromBody] ArticleRequestDto request)
        {
            var response = await _articleService.AddAsync(request);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ArticleResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult GetAllArticles(int pageSize, int pageNumber)
        {
            var response = _articleService.GetAllArticles(pageSize, pageNumber);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<ArticleResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _articleService.GetById(id);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet]
        [Route("~/api/authors/{authorId}/articles/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ArticleResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetArticleByAuthorId(string authorId, string id)
        {
            var response = await _articleService.GetArticleByAuthorId(authorId, id);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateArticle(string id, ArticleRequestDto request)
        {
            await _articleService.UpdateArticle(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            await _articleService.Delete(id);
            return NoContent();
        }
    }
}
