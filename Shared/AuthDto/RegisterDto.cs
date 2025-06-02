using Microsoft.AspNetCore.Http;

namespace Shared.AuthDto
{
    public class RegisterDto
    {
        public required string Email { get; set; } 
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; } 
        public required string FirstName { get; set; } 
        public required string LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public IFormFile? Image { get; set; } = null;
    }
}
