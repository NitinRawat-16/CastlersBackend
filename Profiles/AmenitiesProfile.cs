using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class AmenitiesProfile : Profile
    {
        public AmenitiesProfile()
        {
            CreateMap<DeveloperAmenities, DeveloperAmenitiesDto>();
            CreateMap<DeveloperAmenitiesDto, DeveloperAmenities>();
            CreateMap<DeveloperAmenitiesDetails, DeveloperAmenitiesDetailsDto>();
            CreateMap<DeveloperAmenitiesDetailsDto, DeveloperAmenitiesDetails>();
            CreateMap<DeveloperConstructionSpec, DeveloperConstructionSpecDto>();
            CreateMap<DeveloperConstructionSpecDto, DeveloperConstructionSpec>();
        }
    }
}
