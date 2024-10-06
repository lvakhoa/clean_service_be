using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CleanServiceContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var origins = Environment.GetEnvironmentVariable("ORIGINS")?.Split(",") ?? new[] { "http://localhost:3000" };
const string allowPolicy = "AllowOriginsAndCredentials";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowPolicy, policy =>
    {
        policy.WithOrigins(origins);
        policy.AllowCredentials();
    });
});

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors(allowPolicy);
app.UseAuthorization();
app.UsePathBase(new PathString("/api/v1"));
app.MapControllers();

app.Run();
