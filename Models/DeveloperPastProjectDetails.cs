
namespace castlers.Models;

public class DeveloperPastProjectDetails
{
    public int developerPastProjectDetailsId { get; set; } = -1;
    public int developerId { get; set; } = -1;
    public string projectName { get; set; } = string.Empty;
    public string projectLocation { get; set; } = string.Empty;
    public string reraRegistrationNumber { get; set; } = string.Empty;
    public DateTime projectStartDate { get; set; } = DateTime.Now;
    public DateTime projectEndDate { get; set; } = DateTime.Now;
}
