using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Binds the JwtOptions class to the "ApiSettings:JwtOptions" section of appsettings.json. Allows us to inject this object with IOptions<>
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

// Tells it to use Identity framework with default user and role using Entity Framework Core
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ApplyMigrations();

app.Run();


void ApplyMigrations()
{
    // CreateScope basically gets all the services
    using (var scope = app.Services.CreateScope())
    {
        var _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // If there are any pending migrations, we can simply call Migrate to apply them
        if (_dbContext.Database.GetPendingMigrations().Count() > 0)
        {
            _dbContext.Database.Migrate(); // Applies ALL pending migrations
        }
    }
}
