﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechDaily.Apllication.Interfaces;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;
using TechDaily.Common.Helpers;
using TechDaily.Domain.Entities;

namespace TechDaily.Apllication.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<ApiResponse> AddAsync(AuthorRequestDto request)
        {
            try
            {
                var author = _mapper.Map<Author>(request);
                await _unitOfWork.Repository<Author>().AddAsync(author);
                await _unitOfWork.CommitAsync();

                _cache.Remove("authors");

                _logger.Information($"Successfully added author - {JsonConvert.SerializeObject(author)}");
                return ApiResponse<AuthorResponseDto>
                    .Success(_mapper.Map<AuthorResponseDto>(author), "Successfully created author");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not add author");
            }
        }

        public ApiResponse<PaginatedResponse<AuthorResponseDto>> GetAllAuthors(int pageSize, int pageNumber)
        {
            var authors = _cache.Get("authors") as List<Author>;

            if (authors == null)
            {
                authors = _unitOfWork.Repository<Author>().GetAll().ToList();
                _cache.Set("authors", authors);
            }

            var paginated = Utils.Paginate(authors, ref pageSize, ref pageNumber);
            var paginatedResponse = new PaginatedResponse<AuthorResponseDto>
                { PageItems = _mapper.Map<List<AuthorResponseDto>>(paginated), PageNumber = pageNumber, 
                PageSize = pageSize, TotalCount = authors.Count() };

            return ApiResponse<PaginatedResponse<AuthorResponseDto>>
                .Success(paginatedResponse, "Successfully retrieved authors");
        }

        public async Task<ApiResponse> GetById(string id)
        {
            try
            {
                var author = await _unitOfWork.Repository<Author>()
                    .GetAsync(x => x.Id == id);
                var response = _mapper.Map<AuthorResponseDto>(author);

                return ApiResponse<AuthorResponseDto>
                    .Success(response, $"Successfully retrieved author with Id:{response.Id}");
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not retrieve author");
            }
        }

        public async Task UpdateAuthor(string id, AuthorRequestDto request)
        {
            var author = await _unitOfWork.Repository<Author>()
                    .GetAsync(x => x.Id == id);
            try
            {
                var response = _mapper.Map(request, author);
                _unitOfWork.Repository<Author>().Update(response);
                await _unitOfWork.CommitAsync();

                _logger.Information($"Successfully updated author - {JsonConvert.SerializeObject(author)}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        public async Task Delete(string id)
        {
            var author = await _unitOfWork.Repository<Author>().GetAsync(x => x.Id == id);
            _unitOfWork.Repository<Author>().Delete(author);
            await _unitOfWork.CommitAsync();
        }
    }
}
