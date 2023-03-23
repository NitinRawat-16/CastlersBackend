using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class DeveloperProfile : Profile
    {
        public DeveloperProfile()
        {
            CreateMap<DeveloperDto, Developer>();
            CreateMap<Developer, DeveloperDto>();
        }
    }
}
