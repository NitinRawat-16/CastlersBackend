using castlers.ResponseDtos;

namespace castlers.Common.AzureStorage
{
    public interface IUploadFile
    {
        public Task<SaveDocResponseDto> SaveDoc(IFormFile file, string filePath, bool isUpdate = false);
    }
}
