using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models
{
    public class Developer
    {
        public int? developerId { get; set; }
        public string? mobileNumber { get; set; }
        public string? name { get; set; }
        public int? organisationTypeId { get; set; }
        public string? address { get; set; }
        public string? siteLink { get; set; }
        public string? email { get; set; }
        public int? experienceYear { get; set; }
        [NotMapped]
        public IFormFile? profile { get; set; }
        [NotMapped]
        public IFormFile? extraDoc { get; set; }
        public string? profilePath { get; set; }
        public string? logoPath { get; set; }
        public Guid? registeredDeveloperCode { get; set; }
        public Guid createdBy { get; set; } = new Guid();
        public Guid updatedBy { get; set; } = new Guid();
        public DateTime createdDate { get; set; } = new DateTime().Date;
        public DateTime updatedDate { get; set; } = new DateTime().Date;
        public DeveloperKYC? developerKYC { get; set; }
        public List<PartnerKYC>? partnerKYCs { get; set; }
    }
}
