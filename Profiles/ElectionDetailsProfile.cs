using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class ElectionDetailsProfile : Profile
    {
        public ElectionDetailsProfile()
        {
            CreateMap<ElectionDetails, ElectionDetailsDto>();
            CreateMap<ElectionDetailsDto, ElectionDetails>();
        }
    }
}
