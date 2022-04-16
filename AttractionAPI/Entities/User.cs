namespace AttractionAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Comment Comments { get; set; }
    }
}
