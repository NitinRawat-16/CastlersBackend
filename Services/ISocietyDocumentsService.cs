using castlers.Dtos;
namespace castlers.Services
{
    public interface ISocietyDocumentsService
    {
        public Task<bool> SocietyDocumentsUpload(SocietyDocumentDto societyDocumentDto);
    }
}
