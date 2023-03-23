using castlers.Dtos;
using castlers.Models;
using castlers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredSocietyController : ControllerBase
    {
        private readonly IRegisteredSocietyService _registeredSocietyService;

        public RegisteredSocietyController(IRegisteredSocietyService registeredSocietyService)
        {
            this._registeredSocietyService = registeredSocietyService;
        }

        [HttpGet("getRegisteredSocietylist")]
        public async Task<List<RegisteredSocietyDto>> GetRegisteredSocietyAsync()
        {
            try
            {
                return await _registeredSocietyService.GetRegisteredSocietyAsync();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getRegisteredSociety")]
        public async Task<RegisteredSocietyDto> GetRegisteredSocietyAsync(int registeredSocietyId)
        {
            try
            {
                return await _registeredSocietyService.GetRegisteredSocietyByIdAsync(registeredSocietyId);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("registerSociety")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSocietyAsync ([FromBody] RegisteredSocietyDto registeredSocietyDto)
        {
            if (registeredSocietyDto == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _registeredSocietyService.AddRegisteredSocietyAsync(registeredSocietyDto);

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("UpdateTechnicalDetailsSocietyAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateTechnicalDetailsSocietyAsync([FromBody] UpdateTechnicalDetailsRegisteredSocietyDto updateTechnicalDetailsRegisteredSocietyDto)
        {
            if (updateTechnicalDetailsRegisteredSocietyDto == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _registeredSocietyService.UpdateTechnicalDetailsSocietyAsync(updateTechnicalDetailsRegisteredSocietyDto);

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("UpdateRegisteredSociety")]
        public async Task<IActionResult> UpdateRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto)
        {
            if (registeredSocietyDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _registeredSocietyService.UpdateRegisteredSocietyAsync(registeredSocietyDto);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
    }
}
