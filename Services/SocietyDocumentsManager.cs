using AutoMapper;
using castlers.Common.Encrypt;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.ResponseDtos;
using System.Text.Json;

namespace castlers.Services
{
    public class SocietyDocumentsManager : ISocietyDocumentsService
    {
        private readonly IMapper _mapper;
        private readonly ISecureInformation _secureInformation;
        private readonly ISocietyDocRepository _societyDocRepo;
        public SocietyDocumentsManager(ISocietyDocRepository societyDocRepository, IMapper mapper, ISecureInformation secureInformation)
        {
            _mapper = mapper;
            _secureInformation = secureInformation;
            _societyDocRepo = societyDocRepository;
        }
        public async Task<SaveDocResponseDto> SocietyDocumentsUpload(SocietyDocumentDto societyDocumentDto)
        {
            try
            {
                return await _societyDocRepo.UploadSocietyDoc(societyDocumentDto);
            }
            catch (Exception) { throw; }
        }

        public async Task<List<SocietyDocumentsDetails>> GetSocietyDocumentList(string code)
        {
            List<SocietyDocumentsDetails> societyDocuments = new();
            try
            {
                int registeredSocietyId = -1;
                if (code.Trim().Length > 0)
                {
                    var societyDetails = JsonSerializer.Deserialize<TenderNoticeObj>(_secureInformation.Decrypt(code));
                    if (societyDetails != null)
                    {
                        registeredSocietyId = societyDetails.societyId;
                    }
                }
                societyDocuments = await _societyDocRepo.GetSocietyDocs(registeredSocietyId);
                return societyDocuments;
            }
            catch (Exception) { throw; }

        }
    }
}
