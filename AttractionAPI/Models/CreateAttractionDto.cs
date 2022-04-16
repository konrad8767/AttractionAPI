using System.ComponentModel.DataAnnotations;

namespace AttractionAPI.Models
{
    public class CreateAttractionDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public double Rate { get; set; }
        public int TotalRates { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
