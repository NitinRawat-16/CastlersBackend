using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsService _blogService;
        public BlogsController(IBlogsService blogsService)
        {
            _blogService = blogsService;
        }
        [HttpPost("AddBlogs")]
        public async Task<IActionResult> AddBlogs([FromForm] BlogsDto blogsDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                response.Status = await _blogService.AddBlogsAsync(blogsDto);
            }
            catch (Exception) { throw; }

            return Ok(response);
        }
        [HttpGet("GetAllBlogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            List<BlogsDto> blogs = new List<BlogsDto>();
            try
            {
                blogs = await _blogService.GetAllBlogsAsync();
                return blogs is null ? Ok(new List<BlogsDto>()) : Ok(blogs); ;
            }
            catch (Exception) { throw; }
        }

        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById([FromQuery] int blogId)
        {
            BlogsDto blog = new BlogsDto();
            if (blogId <= 0)
            {
                return BadRequest("Blog Id should not be empty or zero!");
            }
            try
            {
                blog = await _blogService.GetBlogByIdAsync(blogId);
                return blog is null? Ok(new BlogsDto()) : Ok(blog);
            }
            catch (Exception) { throw; }
        }
    }
}
