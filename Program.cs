using castlers.Services;
using castlers.DbContexts;
using castlers.Repository;
using castlers.Common.SMS;
using castlers.Common.Email;
using castlers.Repository.Authentication;
using castlers.Common.AzureStorage;
using castlers.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUploadFile, UploadFile>();
builder.Services.AddTransient<IAuthService,AuthManager>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ILoginService, LoginManager>();
builder.Services.AddTransient<IBlogsService, BlogsManager>();
builder.Services.AddTransient<IBlogsRepository, BlogsRepo>();
builder.Services.AddTransient<ITenderRepository, TenderRepo>();
builder.Services.AddTransient<ITenderService, TenderManager>();
builder.Services.AddTransient<IDeveloperRepository, DeveloperRepo>();
builder.Services.AddTransient<IDeveloperService, DeveloperManager>();
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
//builder.Services.AddTransient<ISMSSender, SMSSender>();


builder.Services.AddDbContext<ApplicationDbContext>();

// Email Configuration
//var emailConfig = builder.Configuration
//        .GetSection("EmailConfiguration")
//        .Get<EmailConfiguration>();
//builder.Services.AddSingleton(emailConfig);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy(name: "CorsPolicy", builder =>
//    {
//        builder.WithOrigins("http://localhost:4200")
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//    });
//});

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
