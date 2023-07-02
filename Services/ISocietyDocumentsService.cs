using castlers.Dtos;
namespace castlers.Services
{
    public interface ISocietyDocumentsService
    {
        public Task<SaveDocResponseDto> SocietyDocumentsUpload(SocietyDocumentDto societyDocumentDto);
    }
}
