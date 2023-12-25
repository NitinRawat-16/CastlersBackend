using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDto, SocietyDocumentsDetails>();
        }
    }
}
