namespace castlers.Dtos
{
    public class DeveloperKYCDto
    {
        public int developerKYCId { get; set; }
        public int developerId { get; set; }
        public int orgnisationTypeId { get; set; }
        public string orgnisationPanNumber { get; set; }
        public string incorporationDocPath { get; set; }
        public string incorporationName { get; set; }
        public string gstNumber { get; set; }
    }
}
