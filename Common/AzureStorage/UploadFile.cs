using Azure;
using castlers.DbContexts;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using castlers.ResponseDtos;

namespace castlers.Common.AzureStorage
{
    public class UploadFile : IUploadFile
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly BlobContainerClient _container;
        public UploadFile(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
            _storageContainerName = configuration.GetValue<string>("AzureStorageContainerName");
            _container = new BlobContainerClient(_storageConnectionString, _storageContainerName); ;
        }
        public async Task<SaveDocResponseDto> SaveDoc(IFormFile file, string filePath, bool isUpdate = false)
        {
            //BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            var fileName = file.FileName;
            //var filePath = string.Format(path + "/{0}", fileName);
            try
            {
                BlobClient client = _container.GetBlobClient(filePath);

                // If want to update the existing file.
                if (isUpdate)
                {
                    await client.DeleteAsync();
                }

                // Adding the file to azure.
                await using (Stream? data = file.OpenReadStream())
                {

                    await client.UploadAsync(data, new BlobHttpHeaders { ContentType = file.ContentType });
                }

                //Fetching the URL of the file.
                var fileUrl = client.Uri.AbsoluteUri;
                return new SaveDocResponseDto
                {
                    Error = "",
                    DocURL = client.Uri.AbsoluteUri,
                    Status = "Successfully",
                    Message = "File Uploaded Successfully at the location."
                };
            }
            catch (RequestFailedException e)
                when (e.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                return new SaveDocResponseDto
                {
                    Error = "Same file name is Exist!",
                    Status = "Failed",
                    DocURL = "",
                    Message = "Same file name is Exist, Continue to Update existing file."
                };
            }
            catch (RequestFailedException e)
            {
                return new SaveDocResponseDto
                {
                    Error = "Unhandled Error Occurred!",
                    Status = "Failed",
                    DocURL = "",
                    Message = e.Message
                };
            }
        }
    }
}
