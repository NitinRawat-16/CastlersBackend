namespace castlers.Dtos
{
    public class ChairmanTenderApprovalDto
    {
        public string Code { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
