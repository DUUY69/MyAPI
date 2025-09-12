using System.ComponentModel.DataAnnotations;

namespace MyAPI.Services.Models
{
    public class LoginUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
