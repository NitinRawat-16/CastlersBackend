namespace castlers.Repository.Authentication
{
    public class AuthenticationRepo : IAuthenticationRepository
    {
        public bool UserExists(string username, string password)
        {
            if ((username == "prathamesh@castlers.co.in" || username =="darshanavaravadekar@gmail.com") 
                && password == "castlers@Jan2023")
            {
                return Convert.ToBoolean(1);

            }
            return Convert.ToBoolean(0);
                        
        }

    }
}
