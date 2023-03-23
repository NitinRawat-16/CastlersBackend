using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperKYCController : ControllerBase
    {
        private readonly IDeveloperKYCService _developerKYCService;

        public DeveloperKYCController(IDeveloperKYCService _developerKYCService)
        {
            this._developerKYCService = _developerKYCService;
        }

        [HttpGet("getDeveloperKYClist")]
        public async Task<List<DeveloperKYCDto>> GetDeveloperKYCAsync()
        {
            try
            {
                return await _developerKYCService.GetAllDeveloperKYCAsync();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getDeveloperKYC")]
        public async Task<DeveloperKYCDto> GetDeveloperKYCAsync(int developerKYCId)
        {
            try
            {
                return await _developerKYCService.GetDeveloperKYCByIdAsync(developerKYCId);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("addDeveloperKYC")]
        public async Task<IActionResult> AddDeveloperKYCAsync(DeveloperKYCDto developerKYCDto)
        {
            if (developerKYCDto == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _developerKYCService.AddDeveloperKYCAsync(developerKYCDto);

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("updateDeveloperKYC")]
        public async Task<IActionResult> UpdateDeveloperDtoAsync(DeveloperKYCDto developerKYCDto)
        {
            if (developerKYCDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _developerKYCService.UpdateDeveloperKYCAsync(developerKYCDto);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
    }
}