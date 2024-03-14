using castlers.Dtos;
using castlers.Repository.Authentication;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterOfInterestController : Controller
    {
        private ILetterOfInterestService _letterOfInterestService;
        public LetterOfInterestController(ILetterOfInterestService letterOfInterestService)
        {
            _letterOfInterestService = letterOfInterestService;
        }

        [AuthorizeAccess("Admin")]
        [HttpPost("SendLetterOfInterest")]
        public async Task<IActionResult> SendLetterOfInterest([FromBody] List<DevDetailsForLetterOfInterest> sendLetterOfInterestDto)
        {
            if (sendLetterOfInterestDto is null)
            {
                return BadRequest("Please select developer.");
            }

            try
            {
                var response = await _letterOfInterestService.LetterOfInterestSendAsync(sendLetterOfInterestDto);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }

        [HttpPost("LetterOfInterestSubmitted")]
        public async Task<IActionResult> LetterOfInterestSubmitted([FromQuery] string code, string developerCode)
        {
            try
            {
                var response = await _letterOfInterestService.LetterOfInterestedReceivedAsync(code, developerCode);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }

        [AuthorizeAccess("Admin")]
        [HttpPost("SendTenderNotice")]
        public async Task<IActionResult> SendTenderNotice([FromBody] SendTenderNoticeDto sendTenderNoticeDto)
        {
            if (sendTenderNoticeDto.SocietyId == null) return BadRequest("Society Id should not be null!");
            try
            {
                var response = await _letterOfInterestService.AddSendTenderNoticeDetails(sendTenderNoticeDto);
                if (response > 0) return Ok(response);
                else return Ok(0);
            }
            catch (Exception) { throw; }
        }
    }
}
