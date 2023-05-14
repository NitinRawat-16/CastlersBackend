using castlers.Dtos;
using castlers.Repository;

namespace castlers.Services
{
    public class SocietyDocumentsManager : ISocietyDocumentsService
    {
        private readonly ISocietyDocRepository _societyDocRepository;
        public SocietyDocumentsManager(ISocietyDocRepository societyDocRepository)
        {
            _societyDocRepository = societyDocRepository;
        }
        public async Task<bool> SocietyDocumentsUpload(SocietyDocumentDto societyDocumentDto)
        {
            try
            {
                return await _societyDocRepository.UploadSocietyDoc(societyDocumentDto);
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}
