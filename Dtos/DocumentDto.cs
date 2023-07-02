namespace castlers.Dtos
{
    public class DocumentDto
    {
        public string? DocumentType { get; set; }
        public string? DocumentSubType { get; set; }
        public IFormFile? DocumentFile { get; set; }
    }
}
