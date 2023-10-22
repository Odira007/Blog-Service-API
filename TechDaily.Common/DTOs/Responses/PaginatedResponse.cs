using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Common.DTOs.Responses
{
    public class PaginatedResponse<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<T> PageItems { get; set; }
        public int TotalCount { get; set; }
    }
}
