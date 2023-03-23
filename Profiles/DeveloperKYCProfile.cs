using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class DeveloperKYCProfile : Profile
    {
        public DeveloperKYCProfile()
        {
            CreateMap<DeveloperKYCDto, DeveloperKYC>();
            CreateMap<DeveloperKYC, DeveloperKYCDto>();
        }
    }
}