using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface ISocietyDocRepository
    {
        public Task<SaveDocResponseDto> UploadSocietyDoc(SocietyDocumentDto documentDto);
        //public Task<string> SaveSocietyDoc(string path, IFormFile file);
        public Task<List<SocietyDocumentsDetails>> GetSocietyDocs(int registeredSocietyId);
        public Task<SaveDocResponseDto> DeleteSocietyDoc(string filePath);
        //public Task<List<DocumentDto>> GetAllDocumentFilesAsync(List<SocietyDocumentsDetails> documentsDetails);
    }
}
