using castlers.Dtos;

namespace castlers.Services
{
    public interface IAdminService
    {
        public Task<AdminDto> AddAdmin(AdminDto adminDto);
    }
}
