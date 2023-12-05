using castlers.Dtos;
using castlers.ResponseDtos;

namespace castlers.Services
{
    public interface ILetterOfInterestService
    {
        public Task<MailResponseDto> LetterOfInterestSendAsync(List<DevDetailsForLetterOfInterest> sendLetterOfInterestDto);
        public Task<MailResponseDto> LetterOfInterestedReceivedAsync(string queryParams, string developerCode);
        public Task<int> AddSendTenderNoticeDetails(SendTenderNoticeDto sendTenderNoticeDto);

    }
}
