using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class BlogsManager : IBlogsService
    {
        private readonly IBlogsRepository _blogsRepository;
        private readonly IMapper _mapper;
        public BlogsManager(IBlogsRepository blogsRepository, IMapper mapper)
        {
            _blogsRepository = blogsRepository;
            _mapper = mapper;
        }
        public async Task<string> AddBlogsAsync(BlogsDto blogsDto)
        {
            try
            {
                var blogDetails = _mapper.Map<Blogs>(blogsDto);
                var result = await _blogsRepository.AddBlogsAsync(blogDetails);

                return Convert.ToInt32(result) > 0 ? "success" : "failed";
            }
            catch (Exception) { throw; }
        }
        public async Task<List<BlogsDto>> GetAllBlogsAsync()
        {
            try
            {
                var blogsList = await _blogsRepository.GetAllBlogsAsync();              
                return _mapper.Map<List<BlogsDto>>(blogsList);
            }
            catch (Exception) { throw; }
        }
        public async Task<BlogsDto> GetBlogByIdAsync(int blogId)
        {
            try
            {
                var blogDetails = await _blogsRepository.GetBlogByIdAsync(blogId);
                return _mapper.Map<BlogsDto>(blogDetails);
            }
            catch (Exception) { throw; }
        }
    }
}
