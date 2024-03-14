using AutoMapper;
using castlers.Dtos;
using castlers.Models;

namespace castlers.Profiles
{
    public class BlogsProfile : Profile
    {
        public BlogsProfile()
        {
            CreateMap<BlogsDto, Blogs>();
            CreateMap<Blogs, BlogsDto>();
        }
    }
}
