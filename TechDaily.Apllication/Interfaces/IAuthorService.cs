using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.Apllication.Interfaces
{
    public interface IAuthorService
    {
        Task<ApiResponse> AddAsync(AuthorRequestDto request);
        ApiResponse<PaginatedResponse<AuthorResponseDto>> GetAllAuthors(int pageSize, int pageNumber);
    }
}
