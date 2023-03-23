namespace castlers.Models
{
    public class DeveloperKYC
    {
        public int developerKYCId { get; set; }
        public int developerId { get; set; }
        public int orgnisationTypeId { get; set; }
        public string orgnisationPanNumber { get; set; }
        public string incorporationDocPath { get; set; }
        public string incorporationName { get; set; }
        public string gstNumber { get; set; }
        public Guid createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public Guid updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
