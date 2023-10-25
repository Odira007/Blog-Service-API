using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.Apllication.Interfaces
{
    public interface IArticleService
    {
        Task<ApiResponse> AddAsync(ArticleRequestDto request);
        ApiResponse<PaginatedResponse<ArticleResponseDto>> GetAllArticles(int pageSize, int pageNumber);
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetArticleByAuthorId(string authorId, string id);
        Task UpdateArticle(string id, ArticleRequestDto request);
        Task Delete(string id);
    }
}
