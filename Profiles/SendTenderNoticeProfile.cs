using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class SendTenderNoticeProfile : Profile
    {
        public SendTenderNoticeProfile()
        {
            CreateMap<SendTenderNotice, SendTenderNoticeDto>();
            CreateMap<SendTenderNoticeDto, SendTenderNotice>();
        }
    }
}
