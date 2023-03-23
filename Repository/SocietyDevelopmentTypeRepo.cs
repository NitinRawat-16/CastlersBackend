using castlers.DbContexts;
using castlers.Models;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class SocietyDevelopmentTypeRepo : ISocietyDevelopmentTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SocietyDevelopmentTypeRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SocietyDevelopmentType>> GetAllSocietyDevelopmentTypeAsync()
        {
            return await _dbContext.SocietyDevelopmentType
               .FromSqlRaw<SocietyDevelopmentType>("GetSocietyDevelopementTypeList")
               .ToListAsync();
        }
    }
}
