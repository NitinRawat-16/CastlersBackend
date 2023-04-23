namespace castlers.Repository.Authentication
{
    public interface IAuthenticationRepository
    {
        public bool UserExists(string userName, string password);
    }
}
