using castlers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyDocumentsController : ControllerBase
    {
        [HttpPost("SocietyDocumentUpload")]
        public Task<bool> SocietyDocumentUpload([FromForm] SocietyDocumentDto documentDto)
        {
            return Task.FromResult(true);
        }

    }
}
