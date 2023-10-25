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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<ApiResponse> AddAsync(TagRequestDto request)
        {
            try
            {
                var tag = _mapper.Map<Tag>(request);
                await _unitOfWork.Repository<Tag>().AddAsync(tag);
                await _unitOfWork.CommitAsync();

                _cache.Remove("tags");

                _logger.Information("New tag was created");
                return ApiResponse<TagResponseDto>
                    .Success(_mapper.Map<TagResponseDto>(tag), "Successfully added tag");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not add tag");
            }
        }

        public ApiResponse<PaginatedResponse<TagResponseDto>> GetAllTags(int pageSize, int pageNumber)
        {
            var tags = _cache.Get("articles") as List<Tag>;

            if (tags == null)
            {
                tags = _unitOfWork.Repository<Tag>().GetAll().ToList();
                _cache.Set("tags", tags);
            }

            var paginated = Utils.Paginate(tags, ref pageSize, ref pageNumber);
            var paginatedResponse = new PaginatedResponse<TagResponseDto>()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                PageItems = _mapper.Map<IEnumerable<TagResponseDto>>(tags).ToList(),
                TotalCount = tags.Count()
            };

            return ApiResponse<PaginatedResponse<TagResponseDto>>
                .Success(paginatedResponse, "Successfully retrieved articles");
        }

        public async Task<ApiResponse> GetById(string id)
        {
            try
            {
                var tag = await _unitOfWork.Repository<Tag>().GetAsync(x => x.Id == id);
                var response = _mapper.Map<TagResponseDto>(tag);

                return ApiResponse<TagResponseDto>
                    .Success(response, $"Successfully retrieved tag with Id: {response.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not retrieve tag");
            }
        }

        public async Task UpdateTag(string id, TagRequestDto request)
        {
            var tag = await _unitOfWork.Repository<Tag>()
                    .GetAsync(x => x.Id == id);
            try
            {
                var response = _mapper.Map(request, tag);
                _unitOfWork.Repository<Tag>().Update(response);
                await _unitOfWork.CommitAsync();

                _logger.Information($"Successfully updated tag - {JsonConvert.SerializeObject(tag)}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        public async Task Delete(string id)
        {
            var article = await _unitOfWork.Repository<Tag>().GetAsync(x => x.Id == id);
            _unitOfWork.Repository<Tag>().Delete(article);
            await _unitOfWork.CommitAsync();
        }
    }
}
