using System.Text.Json.Serialization;

namespace castlers.Dtos
{
    public class loginDto
    {

        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string UserMobileNumber { get; set; }
    }
}
