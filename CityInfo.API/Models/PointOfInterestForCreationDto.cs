using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
