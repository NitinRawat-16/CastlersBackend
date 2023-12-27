using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models
{
    public class PartnerKYC
    {
        public int partnerKYCId { get; set; }
        public int designationTypeId { get; set; }
        public int developerId { get; set; }
        public string? email { get; set; }
        public string? contactNumber { get; set; }
        public string? panCard { get; set; }
        public string? aadharCard { get; set; }
        [NotMapped]
        public IFormFile? partnerFile { get; set; }
        public string? partnerFileUrl { get; set; } 
        public Guid createdBy { get; set; } = new Guid();
        public DateTime createdDate { get; set; } = DateTime.Now;
        public Guid updatedBy { get; set; } = new Guid();
        public DateTime updatedDate { get; set; } = DateTime.Now;
    }
}
