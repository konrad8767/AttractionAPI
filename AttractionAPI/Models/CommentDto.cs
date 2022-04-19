using AttractionAPI.Entities;

namespace AttractionAPI.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public int AttractionId { get; set; }
    }
}
