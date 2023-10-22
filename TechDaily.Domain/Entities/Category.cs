using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
