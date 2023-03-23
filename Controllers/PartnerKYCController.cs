using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerKYCController : ControllerBase
    {
        private readonly IPartnerKYCService _partnerKYCService;

        public PartnerKYCController(IPartnerKYCService _partnerKYCService)
        {
            this._partnerKYCService = _partnerKYCService;
        }

        [HttpGet("getPartnerKYClist")]
        public async Task<List<PartnerKYCDto>> GetPartnerKYCAsync()
        {
            try
            {
                return await _partnerKYCService.GetAllPartnersAsync();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getDeveloperPartnerKYC")]
        public async Task<List<PartnerKYCDto>> GetDeveloperPartnerKYCAsync(int developerId)
        {
            try
            {
                return await _partnerKYCService.GetPartnerByDeveloperAsync(developerId);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("addPartnerKYC")]
        public async Task<IActionResult> AddPartnerKYCAsync(List<PartnerKYCDto> partnerKYCDto)
        {
            if (partnerKYCDto == null || partnerKYCDto.Count == 0)
            {
                return BadRequest();
            }

            try
            {
                var response = await _partnerKYCService.AddPartnerAsync(partnerKYCDto);

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("updatePartnerKYC")]
        public async Task<IActionResult> UpdatePartnerDtoAsync(PartnerKYCDto partnerKYCDto)
        {
            if (partnerKYCDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _partnerKYCService.UpdatePartnerAsync(partnerKYCDto);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
    }
}
