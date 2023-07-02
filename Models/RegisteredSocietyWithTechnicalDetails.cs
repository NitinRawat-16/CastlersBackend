using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class RegisteredSocietyWithTechnicalDetails
    {
        public int registeredSocietyId { get; set; }
        public string? societyRegistrationNumber { get; set; }
        public string? societyName { get; set; }
        public int? existingMemberCount { get; set; }
        public double? age { get; set; }
        public string? plotDimensions { get; set; }
        public double? approchRoadWidth { get; set; } 
        public double? totalCarpetBldgArea { get; set; }
        public Guid createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime updatedDate { get; set; }
        public string? societyRegisteredCode { get; set; }
        public bool? commercialUse { get; set; }
        public bool? residentialUse { get; set; }
        public bool? mixedUse { get; set; }
        public int? numberOfCommercialTenaments { get; set; }
        public int? numberOfResidentialTenaments { get; set; }
        public int? numberOfMixedTenaments { get; set; }
        public double? totalCommercialBuiltUpBldgArea { get; set; }
        public double? totalResidentialBuiltUpBldgArea { get; set; }
        public double? totalMixedBuiltUpBldgArea { get; set; }
        public string? societyDevelopmentSubType { get; set; }
        public string? registeredAddress { get; set; }
        public string? email { get; set; }
        public int? sizeOfPlot { get; set; }
        public string? city { get; set; }
        public string? developmentTypeName { get; set; }
    }
}
