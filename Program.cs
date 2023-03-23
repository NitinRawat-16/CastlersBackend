using castlers.Common.Email;
using castlers.DbContexts;
using castlers.Repository;
using castlers.Services;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
