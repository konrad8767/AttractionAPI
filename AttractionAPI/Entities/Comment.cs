namespace AttractionAPI.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Attraction Attraction { get; set; }
    }
}
