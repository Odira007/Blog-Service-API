using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Domain.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
