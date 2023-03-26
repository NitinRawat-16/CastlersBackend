using castlers.Dtos;
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

        [HttpGet("GetAllRegisteredSocietyMembers")]
        public async Task<List<SocietyMemberDetailsDto>> GetAllRegisteredSocietyMembers()
        {
            try
            {
                return await _societyMemberDetailsService.GetAllRegisteredSocietyMembersListAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("AddRegisteredSocietyNewMembers")]
        public async Task<int> AddMembers([FromForm] SocietyNewMemberDetailsDto memberDetails)
        {
            try
            {
               return await _societyMemberDetailsService.AddRegisteredSocietyNewMembersAsync(memberDetails);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPut("UpdateRegisteredSocietyMember")]
        public async Task<int> UpdateMember([FromBody] UpdateSocietyMemberDto updateSocietyMemberDto )
        {
            try
            {
                return await _societyMemberDetailsService.UpdateRegisteredSocietyMemberAsync(updateSocietyMemberDto);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpDelete("DeleteRegisteredSocietyMemberById")]
        public async Task<int> DeleteMember([FromForm] DeleteSocietyMemberDto deleteSocietyMemberDto )
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
