using System.ComponentModel.DataAnnotations;

namespace AttractionAPI.Models
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
    }
}
