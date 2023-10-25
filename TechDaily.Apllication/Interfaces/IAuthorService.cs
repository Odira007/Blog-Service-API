using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;
using TechDaily.Domain.Entities;

namespace TechDaily.Apllication.Interfaces
{
    public interface IAuthorService
    {
        Task<ApiResponse> AddAsync(AuthorRequestDto request);
        ApiResponse<PaginatedResponse<AuthorResponseDto>> GetAllAuthors(int pageSize, int pageNumber);
        Task<ApiResponse> GetById(string id);
        Task UpdateAuthor(string id, AuthorRequestDto request);
        Task Delete(string id);
    }
}
