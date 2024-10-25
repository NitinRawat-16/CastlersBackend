﻿
namespace castlers.Dtos
{
    public class DeveloperTenderDetailsDto
    {
        public int? registeredSocietyId { get; set; }
        public double? percentageOfIncreaseArea { get; set; }
        public double? quantamOfAreaAtDiscountRate { get; set; }
        public double? expectedDiscountRate { get; set; }
        public double? corpusFund { get; set; }
        public double? rentPerSqFtFlat { get; set; }
        public double? rentPerSqFtOffice { get; set; }
        public double? rentPerSqFtShop { get; set; }
        public int? parkingPerMember { get; set; }
        public string? typeOfProject { get; set; }
        public double? refundableDepositPerMemberForFlat { get; set; }
        public double? refundableDepositPerMemberForOffice { get; set; }
        public double? refundableDepositPerMemberForShop { get; set; }
        public double? shiftingChargesForFlatOfficeShop { get; set; }
        public double? bettermentChargesPerMember { get; set; }

        //public bool? isApprovedSociety { get; set; }
        public string? tenderCode { get; set; }
        public int? developerId { get; set; }
        public IFormFile? developerTenderPdf { get; set; }
        public string? developerTenderPdfPath { get; set; }
        public string? code { get; set; }
        public DeveloperAmenitiesDto DeveloperAmenities { get; set; } = new();
        public DeveloperAmenitiesDetailsDto DeveloperAmenitiesDetails { get; set; } = new();
        public DeveloperConstructionSpecDto DeveloperConstructionSpec { get; set; } = new();
    }
}
