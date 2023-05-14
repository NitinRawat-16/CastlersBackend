﻿using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<List<RegisteredSocietyDto>> GetRegisteredSocietyListAsync()
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

        [HttpGet("getRegisteredSocietyById")]
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

        [HttpGet("getSocietyMemberDesignationList")]
        public async Task<List<SocietyMemberDesignationDto>> GetSocietyMemberDesignationsAsync()
        {
            try
            {
                return await _registeredSocietyService.GetSocietyMemberDesignationList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getRegisteredSocietyInfo/{registeredSocietyCode}")]
        public async Task<ActionResult<RegisteredSocietyDto>> GetRegisteredSocietyInfoAsync(string registeredSocietyCode)
        {
            if (string.IsNullOrEmpty(registeredSocietyCode))
            {
                return BadRequest("Registered society code can not be blank");
            }
            try
            {
                return await _registeredSocietyService.GetRegisteredSocietyInfoAsync(registeredSocietyCode);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("registerSociety")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSocietyAsync([FromBody] RegisteredSocietyDto registeredSocietyDto)
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
        public async Task<IActionResult> UpdateRegisteredSocietyAsync([FromBody] RegisteredSocietyDto registeredSocietyDto)
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
