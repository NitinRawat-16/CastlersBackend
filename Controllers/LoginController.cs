using castlers.Dtos;
using castlers.Models;
using castlers.Common.Email;
using Microsoft.AspNetCore.Mvc;
using castlers.Services.Authentication;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IEmailSender _emailSender;
        public LoginController(ILoginService loginService, IEmailSender emailSender)
        {
            _loginService = loginService;
            _emailSender = emailSender;
        }

        [HttpPost("SocietyLogin")]
        public async Task<bool> RegisteredSocietyLogin(string registeredSocietyCode)
        {
            SendTo sendTo = new SendTo
            {
                Name = "Nitin Rawat",
                Email = "nitinrawatsde@gmail.com",
                EMailType = Common.Enums.EmailTypes.LetterOfInterest
            };
           await _emailSender.SendEmailAsync(sendTo);
            return await Task.FromResult(true);
        }

        [HttpPost("UserLogin")]
        public LoginResponseDto Login(loginDto dto)
        {
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            bool response = true;
            try
            {
                return _loginService.IsUserExists(dto.username, dto.password);
            }
            catch (Exception)
            {
                response = false;
            }
            return loginResponseDto;
        }
    }
}
