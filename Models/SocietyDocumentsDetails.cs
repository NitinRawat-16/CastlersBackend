using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class SocietyDocumentsDetails
    {
        public int registeredSocietyId { get; set; }
        public string? societyDocType { get; set; }
        public string? documentName { get; set; }
        public string? docPath { get; set; }
    }
}
