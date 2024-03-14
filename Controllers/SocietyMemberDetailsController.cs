using castlers.Dtos;
using castlers.Repository.Authentication;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyMemberDetailsController : ControllerBase
    {
        private readonly ISocietyMemberDetailsService _societyMemberDetailsService;

        public SocietyMemberDetailsController(ISocietyMemberDetailsService societyMemberDetailsService)
        {
            _societyMemberDetailsService = societyMemberDetailsService;
        }

        [AuthorizeAccess("Admin")]
        [HttpGet("GetRegisteredSocietyMembersBySocietyId/{registeredSocietyId}")]
        public async Task<List<SocietyMemberDetailsDto>> GetRegisteredSocietyMembers(int registeredSocietyId)
        {
            try
            {
                return await _societyMemberDetailsService.GetRegisteredSocietyMembersBySocietyIdAsync(registeredSocietyId);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [AuthorizeAccess("Admin")]
        [HttpPost("AddRegisteredSocietyNewMembers")]
        public async Task<int> AddRegisteredSocietyNewMembers([FromForm] SocietyNewMemberDetailsDto societyNewMemberDetailsDto)
        {
            try
            {
                return await _societyMemberDetailsService.AddRegisteredSocietyNewMembersAsync(societyNewMemberDetailsDto);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [AuthorizeAccess("Admin")]
        [HttpPut("UpdateRegisteredSocietyMembers")]
        public async Task<int> UpdateRegisteredSocietyMembers([FromBody] List<SocietyMemberDetailsDto> societyMemberDetails)
        {
            try
            {
                return await _societyMemberDetailsService.UpdateRegisteredSocietyMemberAsync(societyMemberDetails);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [AuthorizeAccess("Admin")]
        [HttpPost("DeleteRegisteredSocietyMemberById")]
        public async Task<int> DeleteMember([FromBody] DeleteSocietyMemberDto deleteSocietyMemberDto)
        {
            try
            {
                return await _societyMemberDetailsService.DeleteRegisteredSocietyMemberByIdAsync(deleteSocietyMemberDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
