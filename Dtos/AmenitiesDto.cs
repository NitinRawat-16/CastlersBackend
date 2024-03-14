using castlers.Models;

namespace castlers.Dtos
{
    public class AmenitiesDto
    {
        public string? Code { get; set; }
        public DeveloperAmenitiesDto DeveloperAmenitiesDto { get; set; } = new();
        public DeveloperConstructionSpecDto DeveloperConstructionSpecDto { get; set; } = new();
    }
}
