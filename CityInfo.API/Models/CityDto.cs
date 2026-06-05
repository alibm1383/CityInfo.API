namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int NumberOfPointsOfInterests
        {
            get
            {
                return pointOfInterests.Count;
            }
        }
        public List<PointOfInterestDto> pointOfInterests { get; set; } = new List<PointOfInterestDto>();
    }
}
