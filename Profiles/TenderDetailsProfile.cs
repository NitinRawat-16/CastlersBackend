using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class TenderDetailsProfile : Profile
    {
        public TenderDetailsProfile()
        {
            CreateMap<DeveloperTenderDetails, DeveloperTenderDetailsDto>();
            CreateMap<DeveloperTenderDetailsDto, DeveloperTenderDetails>();
            CreateMap<SocietyTenderDetails, SocietyTenderDetailsDto>();
            CreateMap<SocietyTenderDetailsDto,  SocietyTenderDetails>();
            CreateMap<SocietyApprovedTendersDetails, SocietyApprovedTendersDetailsDto>();
        }
    }
}
