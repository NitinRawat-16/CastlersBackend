using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Dtos
{
    public class DeveloperDto
    {
        public int? developerId { get; set; }
        public string? name { get; set; }
        public int? organisationTypeId { get; set; }
        public string? address { get; set; }
        public string? mobileNumber { get; set; }
        public int? experienceYear { get; set; }
        public string? siteLink { get; set; }
        public string? email { get; set; }
        public string? profilePath { get; set; }
        public string? logoPath { get; set; }
        public IFormFile? profileDoc { get; set; }
        public IFormFile? registrationDoc { get; set; }
        public Guid? registeredDeveloperCode { get; set; }
        public string? prtnName { get; set; }
        public string? prtnDesignationType { get; set; }
        public string? prtnEmail { get; set; }
        public string? prtnContactNumber { get; set; }
        public string? prtnPanCard { get; set; }
        public string? prtnAadharCard { get; set; }
        public IFormFile? prtnDoc { get; set; }


        //public DeveloperKYCDto? developer { get; set; }
        //public List<PartnerKYCDto>? PartnerKYCDtos { get; set; }
    }
}
