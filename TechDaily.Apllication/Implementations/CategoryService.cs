using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Apllication.Interfaces;
using TechDaily.Common.DTOs.Responses;
using TechDaily.Domain.Entities;
using Serilog;
using TechDaily.Common.DTOs.Requests;
using Newtonsoft.Json;
using TechDaily.Common.Helpers;
using System.Linq;

namespace TechDaily.Apllication.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<ApiResponse> AddAsync(CategoryRequestDto request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);
                await _unitOfWork.Repository<Category>().AddAsync(category);
                await _unitOfWork.CommitAsync();

                _cache.Remove("categories");

                _logger.Information($"New category created - {JsonConvert.SerializeObject(category)}");
                return ApiResponse<CategoryResponseDto>
                    .Success(_mapper.Map<CategoryResponseDto>(category), "New category created");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not add new category");
            }
        }

        public ApiResponse<PaginatedResponse<CategoryResponseDto>> GetAllCategories(int pageSize, int pageNumber)
        {
            var categories = _cache.Get("categories") as List<Category>;

            if (categories == null)
            {
                categories = _unitOfWork.Repository<Category>().GetAll().ToList();
                _cache.Set("categories", categories);
            }

            var paginated = Utils.Paginate(categories, ref pageSize, ref pageNumber);
            var paginatedResponse = new PaginatedResponse<CategoryResponseDto>
            {
                PageItems = _mapper.Map<List<CategoryResponseDto>>(paginated),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = categories.Count()
            };

            return ApiResponse<PaginatedResponse<CategoryResponseDto>>
                .Success(paginatedResponse, "Successfully retrieved categories");
        }

        public async Task<ApiResponse> GetById(string id)
        {
            try
            {
                var category = await _unitOfWork.Repository<Category>()
                    .GetAsync(x => x.Id == id, new List<string>() { "Articles" });
                var response = _mapper.Map<CategoryResponseDto>(category);

                return ApiResponse<CategoryResponseDto>
                    .Success(response, $"Successfully retrieved category with Id: {response.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not retrieve category");
            }
        }

        public async Task UpdateCategory(string id, CategoryRequestDto request)
        {
            var category = await _unitOfWork.Repository<Category>()
                    .GetAsync(x => x.Id == id);
            try
            {
                var response = _mapper.Map(request, category);
                _unitOfWork.Repository<Category>().Update(response);
                await _unitOfWork.CommitAsync();

                _logger.Information($"Successfully updated category - {JsonConvert.SerializeObject(category)}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        public async Task Delete(string id)
        {
            var category = await _unitOfWork.Repository<Category>().GetAsync(x => x.Id == id);
            _unitOfWork.Repository<Category>().Delete(category);
            await _unitOfWork.CommitAsync();
        }
    }
}
