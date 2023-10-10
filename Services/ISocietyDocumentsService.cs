using castlers.Dtos;
using castlers.ResponseDtos;

namespace castlers.Services
{
    public interface ISocietyDocumentsService
    {
        public Task<SaveDocResponseDto> SocietyDocumentsUpload(SocietyDocumentDto societyDocumentDto);
    }
}
