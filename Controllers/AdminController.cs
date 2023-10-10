using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("AdminRegistration")]
        public async Task<IActionResult> AddAdmin([FromBody] AdminDto adminDto)
        {
            try
            {
                var response = await _adminService.AddAdmin(adminDto);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }
    }
}
