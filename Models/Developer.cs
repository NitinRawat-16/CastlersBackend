using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models;

public class Developer
{
    public Developer()
    {
        DeveloperPastProjectDetails = new List<DeveloperPastProjectDetails>();
    }
    public int? developerId { get; set; }
    public string? mobileNumber { get; set; }
    public string? name { get; set; }
    public int? organisationTypeId { get; set; }
    public string? address { get; set; }
    public string? siteLink { get; set; }
    public string? email { get; set; }
    public int? experienceYear { get; set; }
    [NotMapped]
    public IFormFile? profile { get; set; }
    [NotMapped]
    public IFormFile? extraDoc { get; set; }
    [NotMapped]
    public IFormFile? logo { get; set; }
    public string? profilePath { get; set; }
    public string? logoPath { get; set; }
    public string? registeredDeveloperCode { get; set; }
    public Guid createdBy { get; set; } = new Guid();
    public Guid updatedBy { get; set; } = new Guid();
    public DateTime createdDate { get; set; } = new DateTime().Date;
    public DateTime updatedDate { get; set; } = new DateTime().Date;
    public DeveloperKYC? developerKYC { get; set; }
    public List<PartnerKYC>? partnerKYCs { get; set; }

    // Developer module modification changes
    public int? projectsInHand { get; set; }
    public int? numberOfRERARegisteredProjects { get; set; }
    public int? totalCompletedProjects { get; set; }
    public string? totalConstructionAreaDevTillToday { get; set; } = string.Empty;
    public string? sizeOfTheLargestProjectHandled { get; set; } = string.Empty;
    public string? experienceInHighRiseBuildings { get; set; } = string.Empty;
    public string? avgTurnOverforLastThreeYears { get; set; } = string.Empty;
    public bool? affilicationToAnyDevAssociation { get; set; } = false;
    public string? affilicationDevAssociationName { get; set; } = string.Empty;
    [NotMapped]
    public IFormFile? awardsAndRecognitionDoc { get; set; } = null;
    public string? awardsAndRecognition { get; set; } = string.Empty;
    public bool? haveBusinessInMultipleCities { get; set; } = false;
    public List<DeveloperPastProjectDetails> DeveloperPastProjectDetails { get; set; }
}
