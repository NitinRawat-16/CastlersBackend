using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("AddNewMembers")]
        public async Task<int> AddMembers([FromForm] NewMemberDetailsDto memberDetails)
        {
            try
            {
               return await _societyMemberDetailsService.AddRegisteredSocietyMemberAsync(memberDetails);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPut()]
        public async Task<int> UpdateMember([FromForm] UpdateSocietyMemberDto updateSocietyMemberDto )
        {
            try
            {
                //return await _societyMemberDetailsService;
            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        
    }
}
