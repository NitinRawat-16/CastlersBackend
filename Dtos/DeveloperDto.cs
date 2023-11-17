
namespace castlers.Dtos
{
    public class DeveloperDto
    {
        public DeveloperDto()
        {
            DeveloperPastProjectDetails = new List<DeveloperPastProjectDetailsDto>();
            PartnerKYCDetails = new List<PartnerKYCDto>();
        }
        public int? developerId { get; set; }
        public string? name { get; set; }
        public int? organisationTypeId { get; set; }
        public string? address { get; set; }
        public string? mobileNumber { get; set; }
        public int? experienceYear { get; set; }
        public string? siteLink { get; set; }
        public string? email { get; set; }
        public string? profilePath { get; set; }
        public string? logoPath { get; set; }
        public IFormFile? logo { get ; set; }
        public IFormFile? profileDoc { get; set; }
        public IFormFile? registrationDoc { get; set; }
        public Guid? registeredDeveloperCode { get; set; }
        public string? prtnName { get; set; }
        public string? prtnDesignationType { get; set; }
        public string? prtnEmail { get; set; }
        public string? prtnContactNumber { get; set; }
        public string? prtnPanCard { get; set; }
        public string? prtnAadharCard { get; set; }
        public IFormFile? prtnDoc { get; set; }

        // Developer module modification changes
        public int? projectsInHand { get; set; }
        public int? numberOfRERARegisteredProjects { get; set; }
        public int? totalCompletedProjects { get; set; }
        public string? totalConstructionAreaDevTillToday { get; set; } = string.Empty;
        public string? sizeOfTheLargestProjectHandled { get; set; } = string.Empty;
        public bool? experienceInHighRiseBuildings { get; set; } = false;
        public string? avgTurnOverforLastThreeYears { get; set; } = string.Empty;
        public bool? affilicationToAnyDevAssociation { get; set; } = false;
        public string? affilicationDevAssociationName { get; set; } = string.Empty;
        public IFormFile? awardsAndRecognitionDoc { get; set; } = null;
        public string? awardsAndRecognition { get; set; } = string.Empty;
        public bool? haveBusinessInMultipleCities { get; set; } = false;
        public List<DeveloperPastProjectDetailsDto> DeveloperPastProjectDetails { get; set; }
        public List<PartnerKYCDto> PartnerKYCDetails { get; set; }

        //public DeveloperKYCDto? developer { get; set; }
    }
}
