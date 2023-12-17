using System.Text;
using castlers.Services;
using castlers.DbContexts;
using castlers.Repository;
using castlers.Common.SMS;
using castlers.Common.Email;
using castlers.Common.Encrypt;
using castlers.Common.AzureStorage;
using Microsoft.IdentityModel.Tokens;
using castlers.Services.Authentication;
using castlers.Repository.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

#region Add Transient
// Add services to the container.
builder.Services.AddTransient<ISMSSender, SMSSender>();
builder.Services.AddTransient<IUploadFile, UploadFile>();
builder.Services.AddTransient<IUserRepository, UserRepo>();
builder.Services.AddTransient<IAuthService, AuthManager>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ILoginService, LoginManager>();
builder.Services.AddTransient<IBlogsService, BlogsManager>();
builder.Services.AddTransient<IBlogsRepository, BlogsRepo>();
builder.Services.AddTransient<IAdminService, AdminManager>();
builder.Services.AddTransient<IAdminRepository, AdminRepo>();
builder.Services.AddTransient<ITenderRepository, TenderRepo>();
builder.Services.AddTransient<ITenderService, TenderManager>();
builder.Services.AddTransient<IDeveloperRepository, DeveloperRepo>();
builder.Services.AddTransient<IDeveloperService, DeveloperManager>();
builder.Services.AddTransient<IAmenitiesRepository, AmenitiesRepo>();
builder.Services.AddTransient<IAmenitiesService, AmenitiesManager>();
builder.Services.AddTransient<ISecureInformation, SecureInformation>();
builder.Services.AddTransient<ISocietyDocRepository, SocietyDocRepo>();
builder.Services.AddTransient<IPartnerKYCService, PartnerKYCManager>();
builder.Services.AddTransient<IPartnerKYCRepository, PartnerKYCRepo>();
builder.Services.AddTransient<IDeveloperKYCService, DeveloperKYCManager>();
builder.Services.AddTransient<IDeveloperKYCRepository, DeveloperKYCRepo>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepo>();
builder.Services.AddTransient<ISocietyDocumentsService, SocietyDocumentsManager>();
builder.Services.AddTransient<ILetterOfInterestRepository, LetterOfInterestRepo>();
builder.Services.AddTransient<ILetterOfInterestService, LetterOfInterestManager>();
builder.Services.AddTransient<IRegisteredSocietyRepository, RegisteredSocietyRepo>();
builder.Services.AddTransient<IRegisteredSocietyService, RegisteredSocietyManager>();
builder.Services.AddTransient<ISocietyMemberDetailsService, SocietyMemberDetailsManager>();
builder.Services.AddTransient<ISocietyMemberDetailsRepository, SocietyMemberDetailsRepo>();
builder.Services.AddTransient<ISocietyDevelopmentTypeRepository, SocietyDevelopmentTypeRepo>();
builder.Services.AddTransient<ISocietyDevelopmentTypeService, SocietyDevelopmentTypeManager>();
#endregion


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret_key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthentication();
#endregion

builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
