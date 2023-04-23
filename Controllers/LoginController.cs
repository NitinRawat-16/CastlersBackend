using Azure.Identity;
using castlers.Dtos;
using castlers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;

namespace castlers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private readonly GraphServiceClient _graphServiceClient;
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("SocietyLogin")]
        public async Task<bool> RegisteredSocietyLogin(string registeredSocietyCode)
        {      
            return await Task.FromResult(true);
        }

        [HttpPost("UserLogin")]
        public LoginResponseDto Login(loginDto dto)
        {
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            bool response = true ;
            try
            {
                response =  _loginService.IsUserExists(dto.username, dto.password);                     
            }
            catch (Exception ex)
            {
                response = false;
            }

            if(response)
            {
               loginResponseDto.role = "Admin";
            }
            return loginResponseDto;
        }
    }
}
