using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Dtos;

public class DeveloperPastProjectDetailsDto
{
    public int developerPastProjectDetailsId { get; set; } = -1;
    public int developerId { get; set; } = -1;
    public string projectName { get; set; } = string.Empty;
    public string projectLocation { get; set; } = string.Empty;
    public string reraRegistrationNumber { get; set; } = string.Empty;
    [NotMapped]
    public IFormFile? RERACertificate { get; set; }
    public string reraCertificateUrl { get; set; } = string.Empty;
    public DateTime projectStartDate { get; set; } = DateTime.Now;
    public DateTime projectEndDate { get; set; } = DateTime.Now;
}
