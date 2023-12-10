using castlers.Dtos;
using castlers.Models;
using castlers.ResponseDtos;

namespace castlers.Services
{
    public interface IRegisteredSocietyService
    {
        public Task<List<RegisteredSocietyDto>> GetRegisteredSocietyAsync();
        public Task<RegisteredSocietyDto> GetRegisteredSocietyByIdAsync(int Id);
        public Task<SocietyRegistrationResponseDto> AddRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto);
        public Task<int> UpdateRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto);
        public Task<int> DeleteRegisteredSocietyAsync(int Id);
        public Task<List<SocietyMemberDesignationDto>> GetSocietyMemberDesignationList();
        public Task<RegisteredSocietyDto> GetRegisteredSocietyInfoAsync(string registeredSocietyId);
        public Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto technicalDetailsRegisteredSocietyDto);
        public Task<SocietyInfoViewDto> GetRegSocietyInfoWithDocDetailsAsync(int registeredSocietyId);
        public Task<RegisteredSocietyTechnicalDetails> GetRegisteredSocietyTechnicalDetails(int registeredSocietyId);
        public Task<RegisteredSocietyWithTechnicalDetails> GetRegisteredSocietyWithTechnicalDetails(int societyId);
        public Task<GetInterestedDevelopersForTenderNoticeDto> GetSocietyLetterOfInterestReceived(int registeredSocietyId);
        public Task<RegisteredSocietyWithTechnicalDetails?> VerifyGetSocietyDetailsURL(string code);
        public Task<SocietyTenderDetailsDto?> GetTenderDetailsBySocietyId(int registeredSocietyId);
    }
}
