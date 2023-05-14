using castlers.Dtos;

namespace castlers.Repository
{
    public interface ISocietyDocRepository
    {
        public Task<bool> UploadSocietyDoc(SocietyDocumentDto documentDto);
        //public Task<string> SaveSocietyDoc(string path, IFormFile file);
        public Task<IFormFile> DownloadSocietyDoc(string filePath);
        public Task<string> DeleteSocietyDoc(string filePath);
    }
}
