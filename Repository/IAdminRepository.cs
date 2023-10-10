using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface IAdminRepository
    {
        public Task<int> AddAdmin(Admin admin);
    }
}
