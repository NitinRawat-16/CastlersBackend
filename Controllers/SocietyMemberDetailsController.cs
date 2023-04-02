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

        [HttpPost("AddRegisteredSocietyNewMembers/{registeredSocietyId}")]
        public async Task<int> AddRegisteredSocietyNewMembers(int registeredSocietyId, IFormFile memberDetails)
        {
            try
            {
                SocietyNewMemberDetailsDto societyNewMemberDetailsDto = new SocietyNewMemberDetailsDto
                {
                    societyId = registeredSocietyId,
                    societyNewMemberDetails = memberDetails
                };
               return await _societyMemberDetailsService.AddRegisteredSocietyNewMembersAsync(societyNewMemberDetailsDto);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPut("UpdateRegisteredSocietyMembers")]
        public async Task<int> UpdateRegisteredSocietyMembers([FromBody] List<SocietyMemberDetailsDto> societyMemberDetails )
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

        [HttpPost("DeleteRegisteredSocietyMemberById")]
        public async Task<int> DeleteMember([FromBody] DeleteSocietyMemberDto deleteSocietyMemberDto )
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
