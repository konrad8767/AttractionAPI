namespace AttractionAPI.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AttractionId { get; set; }
        public virtual Attraction Attraction { get; set; }
    }
}
