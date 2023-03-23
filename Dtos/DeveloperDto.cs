namespace castlers.Dtos
{
    public class DeveloperDto
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

        public DeveloperKYCDto developer { get; set; }
        public List<PartnerKYCDto> PartnerKYCDtos {get;set;}
    }
}
