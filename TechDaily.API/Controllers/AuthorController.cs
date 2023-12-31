﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using TechDaily.Apllication.Interfaces;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;
using TechDaily.Domain.Entities;

namespace TechDaily.API.Controllers
{
    [Route("api/authors")]
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
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<AuthorResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult GetAllAuthors(int pageSize, int pageNumber)
        {
            var response = _authorService.GetAllAuthors(pageSize, pageNumber);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AuthorResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _authorService.GetById(id);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateAuthor(string id, AuthorRequestDto request)
        {
            await _authorService.UpdateAuthor(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            await _authorService.Delete(id);
            return NoContent();
        }
    }
}
