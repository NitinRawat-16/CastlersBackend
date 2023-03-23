using castlers.Models;

namespace castlers.Repository
{
    public interface ISocietyDevelopmentTypeRepository
    {
        public Task<List<SocietyDevelopmentType>> GetAllSocietyDevelopmentTypeAsync();
    }
}
