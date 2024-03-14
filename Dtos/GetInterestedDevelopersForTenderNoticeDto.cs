namespace castlers.Dtos
{
    public class GetInterestedDevelopersForTenderNoticeDto
    {
        public GetInterestedDevelopersForTenderNoticeDto()
        {
            InterestedDevelopers = new List<ViewLetterOfInterestReceivedDto>();
        }
        public string? tenderCode { get; set; }
        public List<ViewLetterOfInterestReceivedDto> InterestedDevelopers { get; set; }
    }
}
