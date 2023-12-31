﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.Apllication.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse> AddAsync(CategoryRequestDto request);
        ApiResponse<PaginatedResponse<CategoryResponseDto>> GetAllCategories(int pageSize, int pageNumber);
        Task<ApiResponse> GetById(string id);
        Task UpdateCategory(string id, CategoryRequestDto request);
        Task Delete(string id);
    }
}
