using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminDto, Admin>();
            CreateMap<Admin, AdminDto>();
        }
    }
}
