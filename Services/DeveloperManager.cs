using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.Common.Email;
using castlers.ResponseDtos;
using castlers.Common.AzureStorage;
using System.Security.Cryptography;

namespace castlers.Services;

public class DeveloperManager : IDeveloperService
{
    #region User Defined Variables
    private readonly IMapper _mapper;
    private readonly IUploadFile _uploadFile;
    private readonly IEmailSender _emailSender;
    private readonly IPartnerKYCService _partnerKYCService;
    private readonly IDeveloperRepository _developerRepository;
    private readonly IRegisteredSocietyService _registeredSocietyService;
    private readonly ILetterOfInterestRepository _letterOfInterestRepository;
    public DeveloperManager(IDeveloperRepository developerRepository, IMapper mapper, IPartnerKYCService partnerKYCService, IEmailSender emailSender, IConfiguration configuration, IRegisteredSocietyService registeredSocietyService, ILetterOfInterestRepository letterOfInterestRepository, IUploadFile uploadFile)
    {
        _mapper = mapper;
        _uploadFile = uploadFile;
        _emailSender = emailSender;
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
        try
        {
            //var developer = _mapper.Map<DeveloperDto, Developer>(developerDto);
            int result = 0;
            if (developerDto.name != null && developerDto.logo != null)
            {
                developerDto.logoPath = await UploadFile(developerDto.name, "Logo", developerDto.logo);
            }

            if (developerDto.name != null && developerDto.profileDoc != null)
            {
                developerDto.profilePath = await UploadFile(developerDto.name, "ProfileDocument", developerDto.profileDoc);
            }

            if (developerDto.name != null && developerDto.awardsAndRecognitionDoc != null)
            {
                developerDto.awardsAndRecognition = await UploadFile(developerDto.name, "AwardsRecognition", developerDto.awardsAndRecognitionDoc);
            }

            Developer developer = new()
            {
                name = developerDto.name,
                organisationTypeId = developerDto.organisationTypeId,
                mobileNumber = developerDto.mobileNumber,
                address = developerDto.address,
                city = developerDto.city,
                siteLink = developerDto.siteLink,
                email = developerDto.email,
                logoPath = string.IsNullOrEmpty(developerDto.logoPath) ? string.Empty : developerDto.logoPath,
                profilePath = string.IsNullOrEmpty(developerDto.profilePath) ? string.Empty : developerDto.profilePath,
                registeredDeveloperCode = GenerateDeveloperCode(developerDto.city ?? ""),
                experienceYear = developerDto.experienceYear,
                profile = developerDto.profileDoc,
                projectsInHand = developerDto.projectsInHand,
                numberOfRERARegisteredProjects = developerDto.numberOfRERARegisteredProjects,
                totalCompletedProjects = developerDto.totalCompletedProjects,
                totalConstructionAreaDevTillToday = developerDto.totalConstructionAreaDevTillToday,
                sizeOfTheLargestProjectHandled = developerDto.sizeOfTheLargestProjectHandled,
                experienceInHighRiseBuildings = developerDto.experienceInHighRiseBuildings,
                avgTurnOverforLastThreeYears = developerDto.avgTurnOverforLastThreeYears,
                affilicationToAnyDevAssociation = developerDto.affilicationToAnyDevAssociation,
                affilicationDevAssociationName = developerDto.affilicationDevAssociationName,
                awardsAndRecognition = developerDto.awardsAndRecognition,
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
                developerDto.DeveloperPastProjectDetails.ForEach(p => p.developerId = developerId);
                await AddDeveloperPastProjects(developerDto.DeveloperPastProjectDetails, developerDto.name);

                developerDto.PartnerKYCDetails.ForEach(p => p.developerId = developerId);
                result = await _partnerKYCService.AddPartnerAsync(developerDto.PartnerKYCDetails, developerDto.name ?? "");

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
        catch (Exception) { throw; }
    }

    public async Task AddDeveloperPastProjects(List<DeveloperPastProjectDetailsDto> developerPastProjectDetails, string? developerName)
    {
        try
        {
            var developerPastProjects = _mapper.Map<List<DeveloperPastProjectDetails>>(developerPastProjectDetails);
            SaveDocResponseDto saveDocResponse = new();
            // Upload Rera certificate and Save certificate url in database.
            foreach (var project in developerPastProjects)
            {
                string filePath = string.Format("{0}/{1}/{2}", developerName, "RERACertifications",
                    project.RERACertificate?.FileName.ToString().Trim() + RandomNumberGenerator.GetInt32(0, 10000).ToString("D5"));
                if (project.RERACertificate != null)
                    saveDocResponse = await _uploadFile.SaveDoc(project.RERACertificate, filePath);

                project.reraCertificateUrl = saveDocResponse.DocURL;
            }

            foreach (var projectDetails in developerPastProjects)
            {
                await _developerRepository.AddDeveloperPastProjects(projectDetails);
            }
        }
        catch (Exception) { throw; }
    }

    public async Task<int> UpdateDeveloperReviewRating(UpdateDeveloperReviewRatingDto updateDeveloperReviewRatingDto)
    {
        try
        {
            return await _developerRepository.UpdateDeveloperReviewRating(updateDeveloperReviewRatingDto.DeveloperId, updateDeveloperReviewRatingDto.ReviewRatingScore);
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

    private string GenerateDeveloperCode(string city, string developerType = "")
    {
        if (city.Trim().Length > 0)
        {
            string code = string.Format("{0}/{1}/{2}", "MH", city.Substring(0, 3), RandomNumberGenerator.GetInt32(0, 10000).ToString("D5"));
            return code.ToUpper();
        }
        return string.Empty;
    }
}