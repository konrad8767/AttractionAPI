using System.ComponentModel.DataAnnotations;

namespace AttractionAPI.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string ConfirmPassowrd { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
