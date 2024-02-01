using castlers.Dtos;
using castlers.Repository.Authentication;
using castlers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyDevelopementTypeController : ControllerBase
    {
        private readonly ISocietyDevelopmentTypeService _societyDevelopmentTypeService;

        public SocietyDevelopementTypeController(ISocietyDevelopmentTypeService societyDevelopmentTypeService)
        {
            _societyDevelopmentTypeService = societyDevelopmentTypeService;
        }

        [AuthorizeAccess("Admin")]
        [HttpGet("getDeveloperlist")]
        public async Task<List<SocietyDevelopmentTypeDto>> GetSocietyDevelopmentTypeAsync()
        {
            try
            {
                return await _societyDevelopmentTypeService.GetAllSocietyDevelopmentTypeAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
