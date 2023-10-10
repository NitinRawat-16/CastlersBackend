using castlers.Models;

namespace castlers.Repository
{
    public interface IUserRepository
    {
        public Task<int> AddUserAsync(User user);
        public Task<User> GetUserAsyncByCode(string code);
    }
}
