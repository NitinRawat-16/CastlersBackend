using AutoMapper;
using castlers.Common.AzureStorage;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using System.Security.Cryptography;

namespace castlers.Services
{
    public class PartnerKYCManager : IPartnerKYCService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPartnerKYCRepository _partnerKYCRepository;
        private readonly IMapper _mapper;
        private readonly IUploadFile _uploadFile;

        public PartnerKYCManager(ApplicationDbContext dbContext, IPartnerKYCRepository partnerKYCRepository, IMapper mapper, IUploadFile uploadFile)
        {
            _dbContext = dbContext;
            _partnerKYCRepository = partnerKYCRepository;
            _mapper = mapper;
            _uploadFile = uploadFile;
        }
        public async Task<int> AddPartnerAsync(List<PartnerKYCDto> partnerKYCDto, string developerName)
        {
            var partnerKYCList = _mapper.Map<List<PartnerKYCDto>, List<PartnerKYC>>(partnerKYCDto);

            // Uploading partner file and save in the database

            foreach(var partner in partnerKYCList)
            {
                if(partner.partnerFile != null)
                {
                    string filePath = string.Format("{0}/{1}/{2}", developerName, "PartnersDoc", partner.partnerFile + RandomNumberGenerator.GetInt32(0, 10000).ToString("D5"));
                    var result = await _uploadFile.SaveDoc(partner.partnerFile, filePath);
                    partner.partnerFileUrl = result.DocURL;
                }
            }
            return await _partnerKYCRepository.AddDeveloperPartnerKYCAsync(partnerKYCList);
        }

        public Task<int> DeletePartnerAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerKYCDto>> GetAllPartnersAsync()
        {
            var partnerKYCList = await _partnerKYCRepository.GetAllDeveloperPartnerKYCAsync();
            return _mapper.Map<List<PartnerKYCDto>>(partnerKYCList);
        }

        public async Task<List<PartnerKYCDto>> GetPartnerByDeveloperAsync(int developerId)
        {
           var partnerKYCs = await _partnerKYCRepository.GetDeveloperPartnersKYCByIdAsync(developerId);
            return _mapper.Map<List<PartnerKYCDto>>(partnerKYCs);
        }

        public async Task<int> UpdatePartnerAsync(PartnerKYCDto partnerKYCDto)
        {
            var partnerKYC = _mapper.Map<PartnerKYCDto, PartnerKYC>(partnerKYCDto);
            return await _partnerKYCRepository.UpdateParnterKYCAsync(partnerKYC);
        }
    }
}
