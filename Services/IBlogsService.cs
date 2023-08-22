using castlers.Dtos;

namespace castlers.Services
{
    public interface IBlogsService
    {
        public Task<string> AddBlogsAsync(BlogsDto blogsDto);
        public Task<List<BlogsDto>> GetAllBlogsAsync();
        public Task<BlogsDto> GetBlogByIdAsync(int blogId);
    }
}
