namespace castlers.Dtos
{
    public class SocietyDocumentDto
    {
        public string? societyName { get; set; }
        public string? type { get; set; }
        public string? subType { get; set; }
        public string? typeOfdocumentName { get; set; }
        public bool isUpdate { get; set; }
        public IFormFile? documentFile { get; set; }
    }
}
