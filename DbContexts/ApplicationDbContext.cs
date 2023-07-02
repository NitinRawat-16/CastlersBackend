using castlers.Models;
using Microsoft.EntityFrameworkCore;

namespace castlers.DbContexts
{
    public class ApplicationDbContext : DbContext
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
        public DbSet<RegisteredSocietyWithTechnicalDetails> RegisteredSocietyWithTechnicalDetails { get; set; }
        public DbSet<SocietyDocumentsDetails> SocietyDocumentsDetails { get; set; }
        public DbSet<RegisteredSocietyTechnicalDetails> RegisteredSocietyTechnicalDetails { get; set; }
        public DbSet<DeveloperTenderDetails> DeveloperTenderDetails { get; set; }
        public DbSet<SocietyTenderDetails> SocietyTenderDetails { get; set; }
        public DbSet<DeveloperTendersDetails> DeveloperTendersDetails { get; set; }
        public DbSet<SocietyApprovedTendersDetails> SocietyApprovedTendersDetails { get; set; }

    }
}
