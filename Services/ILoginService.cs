namespace castlers.Services
{
    public interface ILoginService
    {
        public Task<bool> RegisteredSocietyLogin(string registeredSocietyCode);
        public bool IsUserExists(string username, string password);
    }
}
