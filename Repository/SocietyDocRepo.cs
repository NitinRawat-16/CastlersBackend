using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using castlers.Common.AzureStorage;
using castlers.ResponseDtos;

namespace castlers.Repository
{
    public class SocietyDocRepo : ISocietyDocRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUploadFile _uploadFile;
        private readonly IRegisteredSocietyRepository _regSociety;
        public SocietyDocRepo(ApplicationDbContext dbContext, IUploadFile uploadFile, IRegisteredSocietyRepository regSociety)
        {
            _dbContext = dbContext;
            _uploadFile = uploadFile;
            _regSociety = regSociety;
        }
        public async Task<SaveDocResponseDto> UploadSocietyDoc(SocietyDocumentDto documentDto)
        {
            SaveDocResponseDto saveDocResponseDto = new SaveDocResponseDto();
            var societyDetails = await _regSociety.GetRegisteredSocietyByIdAsync(documentDto.SocietyId);
            IFormFile? file = documentDto.documentFile;
            var filePath = string.Format("{0}/{1}/{2}", societyDetails.societyName, documentDto.subType, documentDto.typeOfdocumentName + ".pdf");
            var responseDto = await _uploadFile.SaveDoc(file, filePath, documentDto.isUpdate);
            int isUpload;

            if (responseDto.Status == "Successfully")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RegisteredSocietyName", societyDetails.societyName));
                parameters.Add(new SqlParameter("@DocumentName", documentDto.typeOfdocumentName));
                parameters.Add(new SqlParameter("@DocPath", responseDto.DocURL));

                try
                {
                    isUpload = await Task.Run(() =>
                      _dbContext.Database
                      .ExecuteSqlRawAsync(@"EXEC [dbo].[SaveSocietyDocument] @RegisteredSocietyName, @DocumentName, @DocPath", parameters.ToArray()));
                }
                catch (Exception e)
                {
                    saveDocResponseDto.Error = "Error occurred while saving in the database!";
                    saveDocResponseDto.Status = "Failed";
                    saveDocResponseDto.DocURL = "";
                    saveDocResponseDto.Message = e.Message;
                    return saveDocResponseDto;
                }
                responseDto.DocURL = "";
                responseDto.Message = "File uploaded and database changes saved successfully.";
            }
            return responseDto;
        }
        public async Task<List<SocietyDocumentsDetails>> GetSocietyDocs(int registeredSocietyId)
        {
            try
            {
                SqlParameter para = new SqlParameter("@RegisteredSocietyId", registeredSocietyId);

                var documentList = await Task.Run(() => _dbContext.SocietyDocumentsDetails
                    .FromSqlRaw<SocietyDocumentsDetails>(@"EXEC GetRegisteredSocietyDocDetails @RegisteredSocietyId", para).ToListAsync());

                return documentList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<SaveDocResponseDto> DeleteSocietyDoc(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
