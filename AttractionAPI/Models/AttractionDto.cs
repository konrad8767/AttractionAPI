using System.Collections.Generic;

namespace AttractionAPI.Models
{
    public class AttractionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public int TotalRates { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
