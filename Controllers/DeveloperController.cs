using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;
using castlers.Repository.Authentication;
using Microsoft.AspNetCore.Authorization;

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

        [AuthorizeAccess("Admin")]
        [HttpGet("getDeveloperlist")]
        public async Task<List<DeveloperDto>> GetDeveloperAsync()
        {
            try
            {
                return await _developerService.GetDeveloperAsync();
            }
            catch { throw; }
        }

        [AuthorizeAccess("Admin")]
        [HttpGet("getDeveloper")]
        public async Task<DeveloperDto> GetDeveloperAsync(int developerId)
        {
            try
            {
                return await _developerService.GetDeveloperByIdAsync(developerId);
            }
            catch { throw; }
        }

 
        [HttpPost("addDeveloper")]
        public async Task<IActionResult> AddDeveloperAsync([FromForm] DeveloperDto developerDto)
        {
            if (developerDto == null || developerDto.name == null || developerDto.name.Trim().Length <= 0)
            {
                return BadRequest();
            }

            try
            {
                var response = await _developerService.AddDeveloperAsync(developerDto);

                return Ok(response);
            }
            catch { throw; }
        }

        [AuthorizeAccess("Admin")]
        [HttpPut("UpdateDeveloper")]
        public async Task<IActionResult> UpdateDeveloperAsync(DeveloperDto developerDto)
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
            catch(Exception ex)
            { 
                throw ex;
            }
        }

        [AuthorizeAccess("Admin")]
        [HttpPost("UpdateDeveloperReviewRating")]
        public async Task<IActionResult> UpdateDeveloperReviewRating(UpdateDeveloperReviewRatingDto updateDeveloperReviewRatingDto)
        {
            if (updateDeveloperReviewRatingDto.DeveloperId <= 0) return BadRequest("Invalid Developer Id!");
            try
            {
                var response = await _developerService.UpdateDeveloperReviewRating(updateDeveloperReviewRatingDto);
                return Ok(response);
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpGet("GetDeveloperListPublic")]
        public async Task<IActionResult> GetDeveloperListPublic()
        {
            try
            {
                return Ok(await _developerService.GetDeveloperListPublic());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}