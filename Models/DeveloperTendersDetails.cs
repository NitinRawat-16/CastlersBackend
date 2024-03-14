using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class DeveloperTendersDetails
    {
        public int? developerId { get; set; }
        public int? registeredSocietyId { get; set; }
        public string? name { get; set; }
        public string? developerTenderPdfPath { get; set; }
    }
}
