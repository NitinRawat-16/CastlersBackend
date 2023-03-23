using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService _developerService)
        {
            this._developerService = _developerService;
        }

        [HttpGet("getDeveloperlist")]
        public async Task<List<DeveloperDto>> GetDeveloperAsync()
        {
            try
            {
                return await _developerService.GetDeveloperAsync();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getDeveloper")]
        public async Task<DeveloperDto> GetDeveloperAsync(int developerId)
        {
            try
            {
                return await _developerService.GetDeveloperByIdAsync(developerId);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("addDeveloper")]
        public async Task<IActionResult> AddDeveloperAsync(DeveloperDto developerDto)
        {
            if (developerDto == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _developerService.AddDeveloperAsync(developerDto);

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("UpdateDeveloper")]
        public async Task<IActionResult> UpdateDeveloperDtoAsync(DeveloperDto developerDto)
        {
            if (developerDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _developerService.UpdateDeveloperAsync(developerDto);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
    }
}