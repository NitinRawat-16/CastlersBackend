namespace castlers.Dtos
{
    public class SaveDocResponseDto
    {
        public string? Message  { get; set; }
        public string? Status {get; set; }
        public string? Error { get; set; }
        public string DocURL { get; set; }
    }
}
