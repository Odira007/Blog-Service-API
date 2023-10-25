using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechDaily.Common.DTOs.Requests
{
    public class ArticleRequestDto
    {
        [Required]
        [Column(TypeName = "ntext")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string TextContent { get; set; }
        public string FeaturedImage { get; set; }
    }
}
