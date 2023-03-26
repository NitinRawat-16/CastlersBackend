using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class SocietyMemberDesignationProfile : Profile
    {
        public SocietyMemberDesignationProfile()
        {
            CreateMap<SocietyMemberDesignation, SocietyMemberDesignationDto>();
            CreateMap<SocietyMemberDesignationDto, SocietyMemberDesignation>();
        }
    }
}
