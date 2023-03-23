using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class NewMemberDetailsProfile : Profile
    {
        public NewMemberDetailsProfile()
        {
            CreateMap<NewMemberDetailsDto, NewMemberDetails>();
            CreateMap<NewMemberDetails, NewMemberDetailsDto>();
        }
        
    }
}
