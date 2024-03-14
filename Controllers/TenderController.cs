using castlers.Dtos;
using castlers.Services;
using castlers.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using castlers.Repository.Authentication;

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
        [AllowAnonymous]
        [HttpGet("GetSocietyApprovedTenders")]
        public async Task<IActionResult> GetSocietyApprovedTenders()
        {
            try
            {
                var tenders = await _tenderService.GetSocietyApprovedTenders();
                return Ok(tenders);
            }
            catch (Exception) { throw; }

        }
        [AuthorizeAccess("Admin")]
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
        [AuthorizeAccess("Admin")]
        [HttpGet("GetSocietyActiveTenderIdBySocietyId")]
        public async Task<IActionResult> GetSocietyActiveTenderIdBySocietyId(int societyId)
        {
            if (societyId < 0) return BadRequest("Society Id should not be null!");
            try
            {
                var response = await _tenderService.GetSocietyActiveTenderIdBySocietyId(societyId);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }
        [AuthorizeAccess("Admin")]
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
        [AuthorizeAccess("Admin")]
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

        [AllowAnonymous]
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
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("VerifyGetTenderDetailURL")] // Verify chairman url for tender details
        public async Task<IActionResult> VerifyGetTenderDetailURL([FromQuery] string code)
        {
            if (code.Length <= 0) return BadRequest("Invalid Request");
            try
            {
                var response = await _tenderService.VerifyGetTenderDetailURL(code);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }

        [AuthorizeAccess("Admin")]
        [HttpPost("UpdateTenderStatus")]
        public async Task<IActionResult> UpdateTenderStatus([FromBody] TenderStatusDto tenderStatusDto)
        {
            if (tenderStatusDto.tenderId <= 0) return BadRequest("Tender id is invalid!");
            try
            {
                var response = await _tenderService.UpdateTenderStatus(tenderStatusDto);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }

        [AllowAnonymous]
        [HttpPost("VerifyDeveloperTenderURL")]  //Verify developer url for tender details
        public IActionResult VerifyDeveloperTenderURL([FromQuery] string code)
        {
            if (code.Length <= 0) BadRequest("Invalid request!");
            try
            {
                var response = _tenderService.VerifyDeveloperTenderURL(code);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }
        [AllowAnonymous]
        [HttpPost("VerifyDeveloperTenderCodeWithURL")]
        public async Task<IActionResult> VerifyDeveloperTenderCodeWithURL([FromBody] DeveloperTenderVerifyDto developerTenderVerifyDto)
        {
            if (developerTenderVerifyDto.tenderCode.Trim().Length <= 0) BadRequest("Tender Code is invalid!");
            try
            {
                var response = await _tenderService.VerifyDeveloperTenderCodeWithURL(developerTenderVerifyDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
