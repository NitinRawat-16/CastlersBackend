namespace castlers.Dtos
{
    public class GenDeveloperDto
    {
        public int? developerId { get; set; }
        public string? name { get; set; }
        public int? organisationTypeId { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public int? experienceYear { get; set; }
        public string? siteLink { get; set; }
        public string? email { get; set; }
        public string? profilePath { get; set; }
        public string? logoPath { get; set; }
        public int? reviewRatingScore { get; set; }


        // Developer module modification changes
        //public string? mobileNumber { get; set; }
        //public int? projectsInHand { get; set; }
        //public int? numberOfRERARegisteredProjects { get; set; }
        //public int? totalCompletedProjects { get; set; }
        //public string? totalConstructionAreaDevTillToday { get; set; } = string.Empty;
        //public string? sizeOfTheLargestProjectHandled { get; set; } = string.Empty;
        //public string? experienceInHighRiseBuildings { get; set; } = string.Empty;
        //public string? avgTurnOverforLastThreeYears { get; set; } = string.Empty;
        //public bool? affilicationToAnyDevAssociation { get; set; } = false;
        //public string? affilicationDevAssociationName { get; set; } = string.Empty;
        //public IFormFile? awardsAndRecognitionDoc { get; set; } = null;
        //public string? awardsAndRecognition { get; set; } = string.Empty;
        //public bool? haveBusinessInMultipleCities { get; set; } = false;
        //public string? lastThreeYearReturns { get; set; }
        //public string? financialSecurityToTheSociety { get; set; }
        public List<DeveloperPastProjectDetailsDto> DeveloperPastProjectDetails { get; set; } = new();
        public List<PartnerKYCDto> PartnerKYCDetails { get; set; } = new();

        //public DeveloperKYCDto? developer { get; set; }
    }
}
