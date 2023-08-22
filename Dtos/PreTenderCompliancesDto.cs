using Microsoft.EntityFrameworkCore;

namespace castlers.Dtos
{
    [Keyless]
    public class PreTenderCompliancesDto
    {
        public string? developerName { get; set; }
        public string? status { get; set; }
    }
}
