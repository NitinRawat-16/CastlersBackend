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

            // Mapping of developer past project details between model and dto.
            CreateMap<DeveloperPastProjectDetails, DeveloperPastProjectDetailsDto>();
            CreateMap<DeveloperPastProjectDetailsDto, DeveloperPastProjectDetails>();
        }
    }
}
