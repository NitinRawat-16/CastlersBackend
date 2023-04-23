namespace castlers.Dtos
{
    public class SocietyDocumentDto
    {
        public int registeredSocietyId { get; set; }
        public string? registeredSocietyCode { get; set; }
        public string? type { get; set; }
        public string? subType { get; set; }
        public string? typeOfDocument { get; set; }
        public IFormFile? documentFile { get; set; }
    }
}
