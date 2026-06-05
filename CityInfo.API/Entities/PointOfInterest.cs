using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public required string Name { get; set; }
        [MaxLength(1000)]
        [Required]
        public required string Description { get; set; }
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public City City { get; set; } = null!;
    }
}
