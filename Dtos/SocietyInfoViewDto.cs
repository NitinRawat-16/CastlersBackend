using castlers.Models;

namespace castlers.Dtos
{
    public class SocietyInfoViewDto
    {
        public RegSocietyWithTechDetailsDto? regSocietyWithTechDetailsDto { get; set; }
        public List<SocietyDocumentsDetails>? allDocuments { get; set; }
        public List<SocietyMemberDetailsDto>? committeeMembers { get; set; }
        public List<SocietyTenderDetailsDto>? societyTendersList { get; set; }
        public List<DeveloperTendersDetails>? developerTendersList { get; set; }
        public List<PreTenderCompliancesDto>? preTenderCompliances { get; set; }
        public List<ViewLetterOfInterestReceivedDto>? letterOfInterestReceived { get; set; }
    }
        //public List<DeveloperTenderDetailsDto>? tenderDetailsList { get; set;}
}
