using castlers.Dtos;
using castlers.Models;
using castlers.ResponseDtos;

namespace castlers.Services
{
    public interface ISocietyDocumentsService
    {
        public Task<SaveDocResponseDto> SocietyDocumentsUpload(SocietyDocumentDto societyDocumentDto);
        public Task<List<SocietyDocumentsDetails>> GetSocietyDocumentList(string code);
    }
}
