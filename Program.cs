using castlers.Common.Email;
using castlers.Common.SMS;
using castlers.DbContexts;
using castlers.Repository;
using castlers.Repository.Authentication;
using castlers.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Graph;
using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IRegisteredSocietyService, RegisteredSocietyManager>();
builder.Services.AddTransient<IRegisteredSocietyRepository, RegisteredSocietyRepo>();
builder.Services.AddTransient<IDeveloperService, DeveloperManager>();
builder.Services.AddTransient<IDeveloperRepository, DeveloperRepo>();
builder.Services.AddTransient<IDeveloperKYCService, DeveloperKYCManager>();
builder.Services.AddTransient<IDeveloperKYCRepository, DeveloperKYCRepo>();
builder.Services.AddTransient<IPartnerKYCService, PartnerKYCManager>();
builder.Services.AddTransient<IPartnerKYCRepository, PartnerKYCRepo>();
builder.Services.AddTransient<ISocietyMemberDetailsService, SocietyMemberDetailsManager>();
builder.Services.AddTransient<ISocietyMemberDetailsRepository, SocietyMemberDetailsRepo>();
builder.Services.AddTransient<ISocietyDevelopmentTypeRepository, SocietyDevelopmentTypeRepo>();
builder.Services.AddTransient<ISocietyDevelopmentTypeService, SocietyDevelopmentTypeManager>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ISMSSender, SMSSender>();
builder.Services.AddTransient<ISocietyDocumentsService, SocietyDocumentsManager>();
builder.Services.AddTransient<ILoginService, LoginManager>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepo>();
builder.Services.AddTransient<ISocietyDocRepository, SocietyDocRepo>();

//builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration)
//    .AddMicrosoftGraph( x=>
//    string tenantId = Configuration.GeT
    
//    )
//    .AddInMemoryTokenCaches();


builder.Services.AddDbContext<ApplicationDbContext>();
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

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
