using System.ComponentModel.DataAnnotations;

namespace AttractionAPI.Models
{
    public class UpdateAttractionDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
