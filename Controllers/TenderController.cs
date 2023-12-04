using castlers.Dtos;
using castlers.Models;
using castlers.ResponseDtos;
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

        [HttpPost("IsTenderExists")]
        public async Task<IActionResult> IsTenderExists([FromBody] TenderCodeDto tenderCodeDto)
        {
            if (tenderCodeDto.tenderCode == null)
            {
                return BadRequest("Tender code should not be empty!");
            }
            int result = 0;
            try
            {
                result = await _tenderService.IsTenderExists(tenderCodeDto.tenderCode);
            }
            catch (Exception) { throw; }
            return Ok(result);

        }

        [HttpGet("GetSocietyTenderDetailsByTenderId")]
        public async Task<IActionResult> GetSocietyTenderDetailsByTenderId(int tenderId)
        {
            if (tenderId < 0) return BadRequest("Tender Id should not be null");
            try
            {
                var tenderDetails = await _tenderService.GetSocietyTenderDetailsByTenderId(tenderId);
                return Ok(tenderDetails);
            }
            catch (Exception) { throw; }
        }

        [HttpGet("GetSocietyActiveTenderIdBySocietyId")]
        public async Task<IActionResult> GetSocietyActiveTenderIdBySocietyId(int societyId)
        {
            if (societyId < 0) return BadRequest("Society Id should not be null!");
            try
            {
                var tenderId = await _tenderService.GetSocietyActiveTenderIdBySocietyId(societyId);
                if (tenderId > 0)
                {
                    return Ok(tenderId);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception) { throw; }
        }

        [HttpPost("ChairmanResponseForTenderDetails")]
        public async Task<IActionResult> ChairmanResponseForTenderDetails([FromBody] ChairmanTenderApprovalDto chairmanTenderApprovalDto)
        {
            if (string.IsNullOrEmpty(chairmanTenderApprovalDto.Code)) return BadRequest("URL not found!");
            try
            {
                var response = await _tenderService.ChairmanResponseforSocietyTenderDetails(chairmanTenderApprovalDto);

                if (response)
                    return Ok(response);
                else
                    return BadRequest("Can not process request");
            }
            catch (Exception) { throw; }
        }

        [HttpPost("VerifyGetTenderDetailURL")]
        public async Task<IActionResult> VerifyGetTenderDetailURL(string code)
        {
            if (code.Length <= 0) return BadRequest("Invalid Request");
            try
            {
                var response = await _tenderService.VerifyGetTenderDetailURL(code);
                return Ok(response); 
            }
            catch (Exception) { throw; }
        }
    }
}
