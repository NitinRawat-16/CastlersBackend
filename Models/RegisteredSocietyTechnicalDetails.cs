using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class RegisteredSocietyTechnicalDetails
    {
        public int? registeredSocietyId { get; set; }
        public string? societyName { get; set; }
        public int? sizeOfPlot { get; set; }
        public string? plotDimensions { get; set; }
        public bool? residentialUse { get; set; }
        public bool? commercialUse { get; set; }
        public bool? mixedUse { get; set; }
        public int? numberOfMixedTenaments { get; set; }
        public int? numberOfCommercialTenaments { get; set; }
        public int? numberOfResidentialTenaments { get; set; }
        public double? totalCommercialBuiltUpBldgArea { get; set; }
        public double? totalResidentialBuiltUpBldgArea { get; set; }
        public double? totalMixedBuiltUpBldgArea { get; set; }
        public double? approchRoadWidth { get; set; }
     
    }
}
