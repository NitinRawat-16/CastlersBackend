using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class SocietyMemberDetailsProfile : Profile
    {
        public SocietyMemberDetailsProfile()
        {
            CreateMap<SocietyMemberDetailsDto, SocietyMemberDetails>();

            CreateMap<SocietyMemberDetails, SocietyMemberDetailsDto>();
        }
    }
}
