using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.Common.Email;

namespace castlers.Services
{
    public class DeveloperManager : IDeveloperService
    {
        #region User Defined Variables
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IPartnerKYCService _partnerKYCService;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IRegisteredSocietyService _registeredSocietyService;
        private readonly ILetterOfInterestRepository _letterOfInterestRepository;
        public DeveloperManager(IDeveloperRepository developerRepository, IMapper mapper, IPartnerKYCService partnerKYCService, IEmailSender emailSender, IConfiguration configuration, IRegisteredSocietyService registeredSocietyService, ILetterOfInterestRepository letterOfInterestRepository)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _configuration = configuration;
            _partnerKYCService = partnerKYCService;
            _developerRepository = developerRepository;
            _registeredSocietyService = registeredSocietyService;
            _letterOfInterestRepository = letterOfInterestRepository;
        }
        #endregion
        public Task<int> DeleteDeveloperAsync(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<DeveloperDto>> GetDeveloperAsync()
        {
            var developerList = await _developerRepository.GetAllDeveloperAsync();
            return _mapper.Map<List<DeveloperDto>>(developerList);
        }
        public async Task<DeveloperDto> GetDeveloperByIdAsync(int Id)
        {
            var developer = await _developerRepository.GetDeveloperByIdAsync(Id);
            return _mapper.Map<DeveloperDto>(developer);
        }
        public Task<int> UpdateDeveloperAsync(DeveloperDto developerDto)
        {
            var developer = _mapper.Map<DeveloperDto, Developer>(developerDto);
            return _developerRepository.UpdateDeveloperAsync(developer);
        }
        public async Task<int> AddDeveloperAsync(DeveloperDto developerDto)
        {
            //var developer = _mapper.Map<DeveloperDto, Developer>(developerDto);
            int result = 0;
            var developer = new Developer
            {
                name = developerDto.name,
                organisationTypeId = developerDto.organisationTypeId,
                mobileNumber = developerDto.mobileNumber,
                address = developerDto.address,
                siteLink = developerDto.siteLink,
                email = developerDto.email,
                logoPath = "",
                profilePath = "",
                registeredDeveloperCode = new Guid(),
                experienceYear = developerDto.experienceYear,
                profile = developerDto.profileDoc,
                extraDoc = developerDto.registrationDoc,
                createdBy = new Guid(),
                createdDate = DateTime.Now,
                updatedBy = new Guid(),
                updatedDate = DateTime.Now,
                developerId = 0
            };

            var developerId = await _developerRepository.AddDeveloperAsync(developer);

            if (developerId > 0)
            {
                var partnerDetails = new PartnerKYCDto
                {
                    designationTypeId = 1,
                    developerId = developerId,
                    email = developerDto.prtnEmail,
                    contactNumber = developerDto.prtnContactNumber,
                    panCard = developerDto.prtnPanCard,
                    aadharCard = developerDto.prtnAadharCard,
                    partnerKYCId = 0
                };
                List<PartnerKYCDto> partnerKYCs = new List<PartnerKYCDto>();
                partnerKYCs.Add(partnerDetails);
                result = await _partnerKYCService.AddPartnerAsync(partnerKYCs);

                #region Commented code
                //try
                //{
                //    List<SqlParameter> parameters = new List<SqlParameter>();
                //    parameters.Add(new("@designationTypeId", partnerDetails.designationTypeId));
                //    parameters.Add(new("@developerId", partnerDetails.developerId));
                //    parameters.Add(new("@email", partnerDetails.email));
                //    parameters.Add(new("@contactNumber", partnerDetails.contactNumber));
                //    parameters.Add(new("@panCard", partnerDetails.panCard));
                //    parameters.Add(new("@aadharCard", partnerDetails.aadharCard));
                //    parameters.Add(new("@createdDate", DateTime.Now));
                //    parameters.Add(new("@updatedDate", DateTime.Now));
                //    parameters.Add(new("@partnerKYCId", partnerDetails.partnerKYCId));

                //    parameters[8].Direction = System.Data.ParameterDirection.Output;

                //    await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC AddPartnerKYCDetails @designationTypeId, @developerId, @email, @contactNumber, @panCard, @aadharCard, @createdDate, @updatedDate, @partnerKYCId OUT", parameters.ToArray())
                //    );
                //    if (parameters[8].Value is DBNull)
                //        return 0;
                //    else
                //        return Convert.ToInt32(parameters[8].Value);
                //}
                //catch (Exception) { throw; }
                #endregion

            }
            return result;
        }
        
    }
}