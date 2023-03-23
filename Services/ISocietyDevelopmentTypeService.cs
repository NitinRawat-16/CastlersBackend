using castlers.Dtos;
using castlers.Models;

namespace castlers.Services
{
    public interface ISocietyDevelopmentTypeService
    {
        public Task<List<SocietyDevelopmentTypeDto>> GetAllSocietyDevelopmentTypeAsync();
    }
}
