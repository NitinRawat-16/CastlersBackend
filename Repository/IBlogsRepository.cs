using castlers.Models;

namespace castlers.Repository
{
    public interface IBlogsRepository
    {
        public Task<string?> AddBlogsAsync(Blogs blogs);
        public Task<List<Blogs>> GetAllBlogsAsync();
        public Task<Blogs?> GetBlogByIdAsync(int id);    
    }
}
