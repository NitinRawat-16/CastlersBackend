using castlers.Common.Enums;

namespace castlers.ResponseDtos
{
    public class SocietyRegistrationResponseDto
    {
        public int RegisteredSocietyId { get; set; }
        public int SaveMemberCount { get; set; }
        public string? Error { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
