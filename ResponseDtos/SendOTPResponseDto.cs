namespace castlers.ResponseDtos
{
    public class SendOTPResponseDto
    {
        public string UserName { get; set; }
        public string UserMobileNumber { get; set; }
        public string? Message { get; set; } = default;
        public string Status { get; set; }
    }
}
