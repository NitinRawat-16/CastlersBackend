using Microsoft.EntityFrameworkCore;

namespace castlers.Dtos
{
    [Keyless]
    public class ViewLetterOfInterestReceivedDto
    {
        public int? developerId { get; set; }
        public string? developerName { get; set; }
        public string? developerCode { get; set; }
        public string? developerPhone { get; set; }
        public string? developerEmail { get; set; }
        public string? developerAddress { get; set; }
    }
}
