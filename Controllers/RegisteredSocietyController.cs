﻿using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using castlers.Repository.Authentication;

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

        [AuthorizeAccess("Admin")]
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

        [AuthorizeAccess("Admin,Member")]
        [HttpGet("getRegisteredSocietyById")]
        public async Task<ActionResult<RegisteredSocietyDto>> GetRegisteredSocietyAsync(int registeredSocietyId)
        {
            if (registeredSocietyId <= 0)
            {
                return BadRequest("Registered Society Id is not valid.");
            }
            try
            {
                var response = await _registeredSocietyService.GetRegisteredSocietyByIdAsync(registeredSocietyId);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [AuthorizeAccess("Admin")]
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

        [AuthorizeAccess("Admin,Member")]
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

        [AuthorizeAccess("Admin")]
        [HttpPost("registerSociety")]
        public async Task<IActionResult> AddSocietyAsync([FromBody] RegisteredSocietyDto registeredSocietyDto)
        {
            if (registeredSocietyDto.societyName == null || registeredSocietyDto.societyName == "string")
            {
                return BadRequest("Society name should not be empty.");
            }

            try
            {
                var response = await _registeredSocietyService.AddRegisteredSocietyAsync(registeredSocietyDto);
                return Ok(response);
            }
            catch { throw; }
        }

        [AuthorizeAccess("Admin")]
        [HttpPost("UpdateTechnicalDetailsSocietyAsync")]
        public async Task<IActionResult> UpdateTechnicalDetailsSocietyAsync
            ([FromBody] UpdateTechnicalDetailsRegisteredSocietyDto updateTechnicalDetailsRegisteredSocietyDto)
        {
            if (updateTechnicalDetailsRegisteredSocietyDto == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await _registeredSocietyService.UpdateTechnicalDetailsSocietyAsync(updateTechnicalDetailsRegisteredSocietyDto);
                return Ok(Convert.ToBoolean(response));
            }
            catch
            {
                throw;
            }
        }

        [AuthorizeAccess("Admin")]
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

        // After login this method will called.

        //[AuthorizeAccess("Admin,Member")]
        [HttpGet("GetRegSocietyInfoViewById/{registeredSocietyId}")]
        public async Task<ActionResult<SocietyInfoViewDto>> GetRegisteredSocietyInfoViewAsync(int registeredSocietyId)
        {
            if (registeredSocietyId <= 0)
                return BadRequest("Incorrect Registered Society Id.");

            try
            {
                return await _registeredSocietyService.GetRegSocietyInfoWithDocDetailsAsync(registeredSocietyId);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [AuthorizeAccess("Admin")]
        [HttpGet("GetRegisterdSocietyTechnicalDetailsById/{registeredSocietyId}")]
        public async Task<IActionResult> GetRegisteredSocietyTechnicalDetails(int registeredSocietyId)
        {
            try
            {
                var societyDetails = await _registeredSocietyService.GetRegisteredSocietyTechnicalDetails(registeredSocietyId);
                return Ok(societyDetails);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [AuthorizeAccess("Admin")]
        [HttpGet("GetRegisteredSocietyWithTechnicalDetails")]
        public async Task<IActionResult> GetRegisteredSocietyWithTechnicalDetails(int registeredSocietyId)
        {
            if (registeredSocietyId < 0) return BadRequest("Registered society id should not be null!");
            try
            {
                var societyDetails = await _registeredSocietyService.GetRegisteredSocietyWithTechnicalDetails(registeredSocietyId);
                return Ok(societyDetails);
            }
            catch (Exception) { throw; }
        }


        [AuthorizeAccess("Admin,Member")]
        [HttpGet("GetSocietyLetterOfInterestReceived")]
        public async Task<IActionResult> GetSocietyLetterOfInterestReceived(int registeredSocietyId)
        {
            if (registeredSocietyId < 0) return BadRequest("Registered society id should not be empty!");
            try
            {
                var letterOfInterestReceived = await _registeredSocietyService.GetSocietyLetterOfInterestReceived(registeredSocietyId);
                return Ok(letterOfInterestReceived);
            }
            catch (Exception) { throw; }
        }
        [AllowAnonymous]
        [HttpPost("VerifyGetSocietyDetailURL")]
        public async Task<IActionResult> VerifyGetSocietyDetailURL([FromQuery] string code)
        {
            if (code.Length <= 0) return BadRequest("Invalid Request");
            try
            {
                var response = await _registeredSocietyService.VerifyGetSocietyDetailsURL(code);
                return Ok(response);
            }
            catch (Exception) { throw; }
        }
        [AllowAnonymous]
        [HttpGet("GetTenderDetailsBySocietyId")]
        public async Task<IActionResult> GetTenderDetailsBySocietyId(int registeredSocietyId)
        {
            if (registeredSocietyId <= 0) return BadRequest("Society Id can't be null");
            try
            {
                var tenderDetails = await _registeredSocietyService.GetTenderDetailsBySocietyId(registeredSocietyId);
                return Ok(tenderDetails);
            }
            catch (Exception)  {throw; }
        }

        [HttpGet("GetSocietyListPublic")]
        public async Task<IActionResult> GetSocietyListPublic()
        {
            try
            {
                return Ok(await _registeredSocietyService.GetSocietyListPublic());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
