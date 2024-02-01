using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class SendTenderNotice
    {
        public int TenderNoticeId { get; set; } = default;
        public int? SocietyId { get; set; }
        public string? SocietyName { get; set; }
        public string? TenderCode { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime? PresentationDate { get; set; }
        public List<int>? SelectedDevelopersId { get; set; }
    }
}
