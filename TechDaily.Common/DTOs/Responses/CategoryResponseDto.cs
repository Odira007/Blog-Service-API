using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Common.DTOs.Responses
{
    public class CategoryResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArticleResponseDto> articles { get; set; } = new List<ArticleResponseDto>();
    }
}
