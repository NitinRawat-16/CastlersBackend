using castlers.Dtos;

namespace castlers.Services
{
    public interface ILetterOfInterestService
    {
        public Task<SendMailResponse> LetterOfInterestSendAsync(List<DevDetailsForLetterOfInterest> sendLetterOfInterestDto);
        public Task<SendMailResponse> LetterOfInterestedReceivedAsync(int developerId, int tenderId, bool interested);
        public Task<int> AddSendTenderNoticeDetails(SendTenderNoticeDto sendTenderNoticeDto);

    }
}
