using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface ILetterOfInterestRepository
    {
        public Task<bool> AddLetterOfInterestSendAsync(List<int> developerId, int societyId, int tenderId);
        public Task<bool> AddLetterOfInterestedReceivedAsync(int developerId, int offerId, bool interested);
        public Task<List<PreTenderCompliancesDto>> GetSocietyPreTenderCompliances(int societyId);
        public Task<List<ViewLetterOfInterestReceivedDto>> GetSocietyLetterOfInterestReceived(int societyId);
        public Task<int> AddSendTenderNoticeDetails(SendTenderNotice sendTenderNotice);
    }
}
