using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.Apllication.Interfaces
{
    public interface ITagService
    {
        Task<ApiResponse> AddAsync(TagRequestDto request);
        ApiResponse<PaginatedResponse<TagResponseDto>> GetAllTags(int pageSize, int pageNumber);
        Task<ApiResponse> GetById(string id);
        Task UpdateTag(string id, TagRequestDto request);
        Task Delete(string id);
    }
}
