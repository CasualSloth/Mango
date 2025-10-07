using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Auto registers all classes that implement Profile in this assembly (such as MappingConfig)
builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

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
