namespace castlers.Models
{
    public class PartnerKYC
    {
        public int partnerKYCId { get; set; }
        public int designationTypeId { get; set; }
        public int developerId { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string panCard { get; set; }
        public string aadharCard { get; set; }
        public Guid createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public Guid updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
