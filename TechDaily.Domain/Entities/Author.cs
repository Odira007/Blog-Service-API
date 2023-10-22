using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
