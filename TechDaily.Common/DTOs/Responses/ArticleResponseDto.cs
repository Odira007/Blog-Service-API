using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Common.DTOs.Responses
{
    public class ArticleResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string FeaturedImage { get; set; }
    }
}
