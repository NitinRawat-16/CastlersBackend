using castlers.Common.Enums;

namespace castlers.ResponseDtos
{
    public class NewMembersSaveResponse
    {
        public int SaveMemberCount { get; set; }
        public string? Error { get; set; }
        public Status Status { get; set; }
        public string? Message { get; set; }
    }
}
