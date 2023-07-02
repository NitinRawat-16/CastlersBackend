using castlers.Dtos;
using castlers.Models;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;



namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderService _tenderService;
        
        public TenderController(ITenderService tenderService)
        {
            _tenderService = tenderService;
        }

        [HttpPost("AddTenderDetails")]
        public async Task<TenderResponseDto> AddSocietyTender([FromForm] SocietyTenderDetailsDto tenderDetailsDto)
        {
            TenderResponseDto tenderResponseDto = new TenderResponseDto();
            string result;
            if (tenderDetailsDto.registeredSocietyId <= 0)
            {
                tenderResponseDto.status = "Select Society!";
            }
            try
            {
                result = await _tenderService.AddSocietyTender(tenderDetailsDto);
            }
            catch (Exception) { throw; }

            tenderResponseDto.status = (result == null || result == "0") ? "failed" : "success";
            return tenderResponseDto;
        }

        [HttpPost("AddDeveloperTender")]
        public async Task<TenderResponseDto> AddDeveloperTender([FromForm] DeveloperTenderDetailsDto tenderDetailsDto)
        {
            TenderResponseDto tenderResponseDto = new TenderResponseDto();
            string result;
            if (tenderDetailsDto.registeredSocietyId <= 0)
            {
                tenderResponseDto.status = "Select Society!";
            }
            try
            {
                result = await _tenderService.AddDeveloperTender(tenderDetailsDto);
            }
            catch (Exception) { throw; }

            tenderResponseDto.status = (result == null || result == "0") ? "failed" : "success";
            return tenderResponseDto;
        }
       
        [HttpGet("GetSocietyApprovedTenders")]
        public async Task<IActionResult> GetSocietyApprovedTenders()
        {
            List<SocietyApprovedTendersDetails> societyTenderDetailsDto = new List<SocietyApprovedTendersDetails>();
            try
            {
                societyTenderDetailsDto = await _tenderService.GetSocietyApprovedTenders();
            }
            catch (Exception) { throw; }

            return Ok(societyTenderDetailsDto);
        }
    }
}
