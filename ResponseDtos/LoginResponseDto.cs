using castlers.ViewModel;

namespace castlers.ResponseDtos
{
    public class LoginResponseDto
    {
        public LoginResponseDto()
        {
            IsSuccess = true;
            Message = "";
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public AuthenticationToken? Data { get; set; }
    }
}
