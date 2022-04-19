using System.ComponentModel.DataAnnotations;

namespace AttractionAPI.Models
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
