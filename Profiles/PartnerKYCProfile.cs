using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class PartnerKYCProfile : Profile
    {
        public PartnerKYCProfile()
        {
            CreateMap<PartnerKYCDto, PartnerKYC>();
            CreateMap<PartnerKYC, PartnerKYCDto>();
        }
    }
}
