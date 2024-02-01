using castlers.Dtos;
using castlers.Services;
using castlers.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using castlers.Repository.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyDocumentsController : ControllerBase
    {
        private readonly ISocietyDocumentsService _societyDocumentsService;
        public SocietyDocumentsController(ISocietyDocumentsService societyDocumentsService)
        {
            _societyDocumentsService = societyDocumentsService;
        }

        [AuthorizeAccess("Admin")]
        [HttpPost("SocietyDocumentUpload")]
        public async Task<SaveDocResponseDto> SocietyDocumentUpload([FromForm] SocietyDocumentDto documentDto)
        {
            if (documentDto.documentFile == null)
            {
                return new SaveDocResponseDto
                {
                    Error = "File Does Exist!",
                    Status = "Failed",
                    DocURL = "",
                    Message = "Please Add File While Saving the Document."
                };
            }

            try
            {
                return await _societyDocumentsService.SocietyDocumentsUpload(documentDto);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [AllowAnonymous]
        [HttpPost("GetSocietyDocumentList")]
        public async Task<IActionResult> GetSocietyDocumentList([FromQuery] string code)
        {
            if (code.Trim().Length <= 0) return BadRequest("Invalid Request!");
            try
            {
                var societyDocuments = await _societyDocumentsService.GetSocietyDocumentList(code);
                return Ok(societyDocuments);
            }
            catch (Exception) { throw; }
        }
    }
}
