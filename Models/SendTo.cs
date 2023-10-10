using castlers.Common.Enums;
using castlers.Dtos;

namespace castlers.Models
{
    public class SendTo
    {
        public string? Name { get; set; } = default;
        public string? Email { get; set; } = default;
        public string? Message { get; set; } = default;
        public string? PhoneNumber { get; set; } = default;
        public EmailTypes EMailType { get; set; }
        public string? SMSType { get; set; } = default;
        public string? SocietyName { get; set; } = default;
        public string? InterestedDevAPI { get; set; } = default;
        public string? UninterestedDevAPI { get; set; } = default;
        public DateTime? SendTenderNoticeEndDate { get; set; } = default;
        public DateTime? SendTenderNoticePublicationDate { get; set; } = default;
        public DateTime? SendTenderNoticeStartDate { get; set; } = default;
        public DateTime? SendTenderNoticePresentationDate { get; set; } = default;
        public string? SendTenderNoticeETenderFormAPI { get; set; } = default;
        public string? TenderCode { get; set; } = default;
        public string? RegisteredSocietyCode { get; set; } = default;
        public RegisteredSocietyWithTechnicalDetails SocietyLetterOfInterestDetails { get; set; } = new RegisteredSocietyWithTechnicalDetails();
    }
}
