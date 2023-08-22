using Microsoft.AspNetCore.Mvc;
using castlers.Services.Authentication;
using castlers.Dtos;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("IsSocietyExists")]
        public IActionResult IsSocietyExists([FromBody] RegSocietyCodeDto regSocietyCodeDto)
        {
            int result = 0;
            if(regSocietyCodeDto.societyCode == null)
            {
                return BadRequest("regSocietyCode is null!");
            }
            try
            {
                result = _authService.IsSocietyExits(regSocietyCodeDto.societyCode);
            }
            catch (Exception) { throw; }

            return Ok(result);
        }
    }
}
