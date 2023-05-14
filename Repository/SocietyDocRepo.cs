using castlers.Dtos;
using Azure.Storage.Blobs;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class SocietyDocRepo : ISocietyDocRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        public SocietyDocRepo(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _storageConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
            _storageContainerName = configuration.GetValue<string>("AzureStorageContainerName");
        }
        public async Task<bool> UploadSocietyDoc(SocietyDocumentDto documentDto)
        {
            IFormFile? file = documentDto.documentFile;
            var filePath = string.Format("{0}/{1}", documentDto.societyName, documentDto.type);
            var fileUrl = await SaveDocument(filePath, file);
            int isUpload;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@RegisteredSocietyName", documentDto.societyName));
            parameters.Add(new SqlParameter("@DocumentName", documentDto.documentName));
            parameters.Add(new SqlParameter("@DocPath", fileUrl));

            try
            {
                isUpload = await Task.Run(() =>
                  _dbContext.Database.ExecuteSqlRawAsync(@"EXEC [dbo].[SaveSocietyDocument] @RegisteredSocietyName, @DocumentName, @DocPath", parameters.ToArray()));
            }
            catch (Exception e)
            {
                throw;
            }
            return Convert.ToBoolean(isUpload);
        }
        public async Task<string> SaveDocument(string path, IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            var fileName = file.FileName;
            var filePath = string.Format(path + "/{0}", fileName);
            try
            {
                BlobClient client = container.GetBlobClient(filePath);
                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }
                var fileUrl = client.Uri.AbsoluteUri;
                return fileUrl;
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public Task<string> DeleteSocietyDoc(string filePath)
        {
            throw new NotImplementedException();
        }
        public Task<IFormFile> DownloadSocietyDoc(string filePath)
        {
            throw new NotImplementedException();
        }

    }
}
