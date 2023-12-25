using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly IVotingService _votingService;
        public VotingController(IVotingService votingService)
        {
            _votingService = votingService;
        }
        [HttpPost("SubmitMembersVoting")]
        public async Task<IActionResult> SubmitMembersVoting([FromBody] SubmitVotingDto submitVoting)
        {
            if (string.IsNullOrEmpty(submitVoting.Code) || submitVoting.DeveloperIds.Count <= 0)
                return BadRequest("Invalid Request!");

            try
            {
                var response = await _votingService.SaveMemberVote(submitVoting);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }

        [HttpPost("VerifyVotingUrl")]
        public async Task<IActionResult> VerifyVotingUrl([FromQuery] string code)
        {
            if (code == null || code.Trim().Length <= 0) return BadRequest("Invalid Request!");
            try
            {
                var response = await _votingService.VerifyVotingUrl(code);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }

    }
}
