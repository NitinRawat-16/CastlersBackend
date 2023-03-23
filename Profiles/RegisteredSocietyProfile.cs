using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class RegisteredSocietyProfile : Profile
    {
        public RegisteredSocietyProfile()
        {
            CreateMap<RegisteredSocietyDto, RegisteredSociety>();
            CreateMap<RegisteredSociety, RegisteredSocietyDto>();
            CreateMap<RegisteredSociety, UpdateTechnicalDetailsRegisteredSocietyDto>();
            CreateMap<UpdateTechnicalDetailsRegisteredSocietyDto, RegisteredSociety>();
        }
    }
}
