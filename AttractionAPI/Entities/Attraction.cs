using System.Collections.Generic;

namespace AttractionAPI.Entities
{
    public class Attraction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public int TotalRates { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
