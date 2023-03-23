using castlers.Models;

namespace castlers.Repository
{
    public interface IPartnerKYCRepository
    {
        public Task<List<PartnerKYC>> GetAllDeveloperPartnerKYCAsync();
        public Task<List<PartnerKYC>> GetDeveloperPartnersKYCByIdAsync(int developerId);
        public Task<int> AddDeveloperPartnerKYCAsync(List<PartnerKYC> partnerKYCs);
        public Task<int> UpdateParnterKYCAsync(PartnerKYC partnerKYC);
        public Task<int> DeleteDeveloperParnterKYCAsync(int Id);
    }
}
