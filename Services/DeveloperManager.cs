using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.Common.Email;
using castlers.Common.AzureStorage;

namespace castlers.Services
{
    public class DeveloperManager : IDeveloperService
    {
        #region User Defined Variables
        private readonly IMapper _mapper;
        private readonly IUploadFile _uploadFile;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IPartnerKYCService _partnerKYCService;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IRegisteredSocietyService _registeredSocietyService;
        private readonly ILetterOfInterestRepository _letterOfInterestRepository;
        public DeveloperManager(IDeveloperRepository developerRepository, IMapper mapper, IPartnerKYCService partnerKYCService, IEmailSender emailSender, IConfiguration configuration, IRegisteredSocietyService registeredSocietyService, ILetterOfInterestRepository letterOfInterestRepository, IUploadFile uploadFile)
        {
            _mapper = mapper;
            _uploadFile = uploadFile;
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
                logoPath = developerDto.logo == null ? string.Empty : await UploadFile(developerDto.name, "Logo", developerDto.logo),
                profilePath = developerDto.profileDoc == null ? string.Empty : await UploadFile(developerDto.name, "ProfileDocument", developerDto.profileDoc),
                registeredDeveloperCode = new Guid().ToString(),
                experienceYear = developerDto.experienceYear,
                profile = developerDto.profileDoc,
                // extraDoc = developerDto.registrationDoc == null ? string.Empty : await UploadFile(developerDto.name, "RegistrationDocument", developerDto.registrationDoc),
                projectsInHand = developerDto.projectsInHand,
                numberOfRERARegisteredProjects = developerDto.numberOfRERARegisteredProjects,
                totalCompletedProjects = developerDto.totalCompletedProjects,
                totalConstructionAreaDevTillToday = developerDto.totalConstructionAreaDevTillToday,
                sizeOfTheLargestProjectHandled = developerDto.sizeOfTheLargestProjectHandled,
                experienceInHighRiseBuildings = developerDto.experienceInHighRiseBuildings,
                avgTurnOverforLastThreeYears = developerDto.avgTurnOverforLastThreeYears,
                affilicationToAnyDevAssociation = developerDto.affilicationToAnyDevAssociation,
                affilicationDevAssociationName = developerDto.affilicationDevAssociationName,
                awardsAndRecognition = developerDto.awardsAndRecognitionDoc == null ? string.Empty :
                                       await UploadFile(developerDto.name, "AwardsRecognition", developerDto.awardsAndRecognitionDoc),
                haveBusinessInMultipleCities = developerDto.haveBusinessInMultipleCities,
                createdBy = new Guid(),
                createdDate = DateTime.Now,
                updatedBy = new Guid(),
                updatedDate = DateTime.Now,
                developerId = 0
            };

            var developerId = await _developerRepository.AddDeveloperAsync(developer);
            if (developerId > 0)
            {
                //List<PartnerKYCDto> partnerKYCs = new List<PartnerKYCDto>();
                //var partnerDetails = new PartnerKYCDto 
                //{
                //    designationTypeId = 1,
                //    developerId = developerId,
                //    email = developerDto.prtnEmail,
                //    contactNumber = developerDto.prtnContactNumber,
                //    panCard = developerDto.prtnPanCard,
                //    aadharCard = developerDto.prtnAadharCard,
                //    partnerKYCId = 0
                //};
                //partnerKYCs.Add(partnerDetails);

                developerDto.DeveloperPastProjectDetails.ForEach(p => p.developerId = developerId);
                await AddDeveloperPastProjects(developerDto.DeveloperPastProjectDetails);

                developerDto.PartnerKYCDetails.ForEach(p => p.developerId = developerId);
                result = await _partnerKYCService.AddPartnerAsync(developerDto.PartnerKYCDetails);

                #region Email
                SendTo sendTo = new SendTo()
                {
                    Name = developerDto.name,
                    Email = developerDto.email,
                    EMailType = Common.Enums.EmailTypes.DeveloperRegister,
                    Message = developerDto.registeredDeveloperCode
                };
                await _emailSender.SendEmailAsync(sendTo);
                #endregion
            }
            return result;
        }
        public async Task AddDeveloperPastProjects(List<DeveloperPastProjectDetailsDto> developerPastProjectDetails)
        {
            try
            {
                var developerPastProjects = _mapper.Map<List<DeveloperPastProjectDetails>>(developerPastProjectDetails);
                foreach (var projectDetails in developerPastProjects)
                {
                    await _developerRepository.AddDeveloperPastProjects(projectDetails);
                }
            }
            catch (Exception) { throw; }
        }
        protected async Task<string> UploadFile(string developerName, string fileType, IFormFile file)
        {
            try
            {
                string filePath = string.Format("{0}/{1}/{2}", developerName, fileType, file.FileName);
                var response = await _uploadFile.SaveDoc(file, filePath);
                return response.DocURL;
            }
            catch (Exception) { throw; }
        }
    }
}