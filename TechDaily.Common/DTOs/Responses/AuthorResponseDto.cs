using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Common.DTOs.Responses
{
    public class AuthorResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
