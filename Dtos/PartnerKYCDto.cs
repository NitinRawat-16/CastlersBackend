using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Dtos
{
    public class PartnerKYCDto
    {
        public int partnerKYCId { get; set; }
        public int designationTypeId { get; set; }
        public int developerId { get; set; }
        public string? email { get; set; }
        public string? contactNumber { get; set; }
        public string? panCard { get; set; }
        public string? aadharCard { get; set; }
        [NotMapped]
        public IFormFile partnerFile { get; set; }
        public string partnerFileUrl { get; set; } = string.Empty;
    }
}
