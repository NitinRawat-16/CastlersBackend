namespace castlers.Dtos
{
    public class VotingDevelopersDto
    {
        public int DeveloperId {  get; set; }
        public string? DeveloperName { get; set; }
        public string? LogoUrl {  get; set; }
        public int Rating { get; set; }
        public string? TenderPdfUrl {  get; set; }
        public string? AmenitiesPdfUrl {  get; set; }
        public string? UserAmenitiesPdfUrl { get; set; }
        public string? ConstructionSpecPdfUrl {  get; set; }
        public string? UserConstructionSpecPdfUrl { get; set; }

    }
}
