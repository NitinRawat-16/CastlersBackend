using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class SocietyDevelopmentTypeProfile : Profile
    {
        public SocietyDevelopmentTypeProfile()
        {
            CreateMap<SocietyDevelopmentTypeDto, SocietyDevelopmentType>();
            CreateMap<SocietyDevelopmentType, SocietyDevelopmentTypeDto>();
        }
    }
}
