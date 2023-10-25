using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Apllication.Interfaces;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;
using TechDaily.Common.Helpers;
using TechDaily.Domain.Entities;

namespace TechDaily.Apllication.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<ApiResponse> AddAsync(ArticleRequestDto request)
        {
            try
            {
                var article = _mapper.Map<Article>(request);
                await _unitOfWork.Repository<Article>().AddAsync(article);
                await _unitOfWork.CommitAsync();

                _cache.Remove("articles");

                _logger.Information("New article was created");
                return ApiResponse<ArticleResponseDto>
                    .Success(_mapper.Map<ArticleResponseDto>(article), "Successfully added article");
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not add article");
            }
        }

        public ApiResponse<PaginatedResponse<ArticleResponseDto>> GetAllArticles(int pageSize, int pageNumber)
        {
            var articles = _cache.Get("articles") as List<Article>;

            if (articles == null)
            {
                articles = _unitOfWork.Repository<Article>().GetAll().ToList();
                _cache.Set("articles", articles);
            }

            var paginated = Utils.Paginate(articles, ref pageSize, ref pageNumber);
            var paginatedResponse = new PaginatedResponse<ArticleResponseDto>()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                PageItems = _mapper.Map<IEnumerable<ArticleResponseDto>>(articles).ToList(),
                TotalCount = articles.Count()
            };

            return ApiResponse<PaginatedResponse<ArticleResponseDto>>
                .Success(paginatedResponse, "Successfully retrieved articles");
        }

        public async Task<ApiResponse> GetArticleByAuthorId(string authorId, string id)
        {
            var author = await _unitOfWork.Repository<Author>().GetAsync(x => x.Id == authorId);
            if (author == null)
            {
                _logger.Error($"Author with the id:{authorId} could not be found");
            }
            try
            {
                var article = _unitOfWork.Repository<Article>().GetAsync(x => x.Id == id && x.AuthorId == authorId);
                var response = _mapper.Map<ArticleResponseDto>(article);

                return ApiResponse<ArticleResponseDto>
                    .Success(response, $"Successfully retrieved article with Id: {response.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not retrieve article");
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            try
            {
                var article = await _unitOfWork.Repository<Article>().GetAsync(x => x.Id == id);
                var response = _mapper.Map<ArticleResponseDto>(article);

                return ApiResponse<ArticleResponseDto>
                    .Success(response, $"Successfully retrieved article with Id: {response.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not retrieve article");
            }
        }

        public async Task UpdateArticle(string id, ArticleRequestDto request)
        {
            var article = await _unitOfWork.Repository<Article>()
                    .GetAsync(x => x.Id == id);
            try
            {
                var response = _mapper.Map(request, article);
                _unitOfWork.Repository<Article>().Update(response);
                await _unitOfWork.CommitAsync();

                _logger.Information($"Successfully updated article - {JsonConvert.SerializeObject(article)}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        public async Task Delete(string id)
        {
            var article = await _unitOfWork.Repository<Article>().GetAsync(x => x.Id == id);
            _unitOfWork.Repository<Article>().Delete(article);
            await _unitOfWork.CommitAsync();
        }
    }
}
