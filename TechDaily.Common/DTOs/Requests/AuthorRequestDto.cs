using System.ComponentModel.DataAnnotations;

namespace TechDaily.Common.DTOs.Requests
{
    public class AuthorRequestDto
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{2,}$", 
            ErrorMessage = "FirstName must begin with a capital letter, and have at least 3 characters")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
