using castlers.Dtos;

namespace castlers.Services
{
    public interface IPartnerKYCService
    {
        public Task<List<PartnerKYCDto>> GetAllPartnersAsync();
        public Task<List<PartnerKYCDto>> GetPartnerByDeveloperAsync(int developerId);
        public Task<int> AddPartnerAsync(List<PartnerKYCDto> partnerKYCDto, string developerName);
        public Task<int> UpdatePartnerAsync(PartnerKYCDto partnerKYCDto);
        public Task<int> DeletePartnerAsync(int Id);
    }
}
