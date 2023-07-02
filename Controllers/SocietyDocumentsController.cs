using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("SocietyDocumentUpload")]
        public async Task<SaveDocResponseDto> SocietyDocumentUpload([FromForm] SocietyDocumentDto documentDto)
        {
            if(documentDto.documentFile == null)
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
    }
}
