using castlers.Dtos;
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
        public DbSet<PreTenderCompliancesDto> PreTenderCompliances { get; set; }
        public DbSet<ViewLetterOfInterestReceivedDto> LetterOfInterestReceived { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<UserOTPDetails> UserOTPDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DeveloperAmenities> DeveloperAmenities { get; set; }
        public DbSet<DeveloperAmenitiesDetails> DeveloperAmenitiesDetails { get; set; }
        public DbSet<DeveloperConstructionSpec> DeveloperConstructionSpecs { get; set; }

    }
}
