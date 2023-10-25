using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TechDaily.Common.DTOs.Requests;
using TechDaily.Common.DTOs.Responses;
using TechDaily.Domain.Entities;

namespace TechDaily.Apllication
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<AuthorRequestDto, Author>();
            CreateMap<Author, AuthorResponseDto>();
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<TagRequestDto, Tag>();
            CreateMap<Tag, TagResponseDto>();
            CreateMap<ArticleRequestDto, Article>();
            CreateMap<Article, ArticleResponseDto>();
        }
    }
}
