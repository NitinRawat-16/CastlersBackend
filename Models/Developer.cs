namespace castlers.Models
{
    public class Developer
    {
        public int developerId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string logoPath { get; set; }
        public string siteLink { get; set; }
        public int sizeOfPlot { get; set; }
        public string email { get; set; }
        public string profilePath { get; set; }
        public int mobileNumber { get; set; }
        public Guid registeredDeveloperCode { get; set; }
        public Guid createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public Guid updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
        public DeveloperKYC developerKYC { get; set; }
        public List<PartnerKYC> partnerKYCs { get; set; }

    }
}
