using castlers.Models;
using Microsoft.EntityFrameworkCore;

namespace castlers.DbContexts
{
    public class ApplicationDbContext :  DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<RegisteredSociety> RegisteredSociety { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<DeveloperKYC> DeveloperKYC { get; set; }
        public DbSet<PartnerKYC> PartnerKYC { get; set; }
        public DbSet<SocietyDevelopmentType> SocietyDevelopmentType { get; set; }
        public DbSet<SocietyMemberDetails> SocietyMemberDetails { get; set; }
        public DbSet<SocietyMemberDesignation> SocietyMemberDesignations { get; set; }

    }
}
