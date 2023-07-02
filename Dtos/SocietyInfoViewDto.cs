using castlers.Models;

namespace castlers.Dtos
{
    public class SocietyInfoViewDto
    {
        public RegSocietyWithTechDetailsDto? regSocietyWithTechDetailsDto { get; set; }
        public List<SocietyDocumentsDetails>? allDocuments { get; set; }
        public List<SocietyMemberDetailsDto>? committeeMembers { get; set; }
        public List<SocietyTenderDetailsDto>? societyTenderList { get; set; }
        public List<DeveloperTenderDetailsDto>? tenderDetailsList { get; set;}
        public List<DeveloperTendersDetails>? developerTendersList { get; set; }
    }
}
