using System.Net;
using System.Security.Claims;
using CleanService.Src.Constant;
using CleanService.Src.Filters;
using CleanService.Src.Middlewares;
using CleanService.Src.Models;
using CleanService.Src.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<CleanServiceContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

var origins = Environment.GetEnvironmentVariable("ORIGINS")?.Split(",")
              ?? new[] { "http://localhost:3000" };
const string allowPolicy = "AllowCors";
builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(allowPolicy, policy =>
        {
            policy
                .WithOrigins(origins)
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthPolicy.IsAdmin,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString()); });

    options.AddPolicy(AuthPolicy.IsHelper,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Helper.ToString()); });

    options.AddPolicy(AuthPolicy.IsCustomer,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Customer.ToString()); });
});

builder.Services
    .AddAppDependency(builder.Configuration);

builder.Services
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler();
}

app.UseCors(allowPolicy);

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/v1"))
    {
        await next();
    }
    else
    {
        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = HttpStatusCode.NotFound,
            Message = "Route not found",
            ExceptionCode = ExceptionConvention.NotFound,
        });
    }
});

app.UsePathBase(new PathString("/api/v1"));
app.UseRouting();

app.UseStatusCodePages(new StatusCodePagesOptions()
{
    HandleAsync = async (ctx) =>
    {
        if (ctx.HttpContext.Response.StatusCode == 404)
        {
            await ctx.HttpContext.Response.WriteAsJsonAsync(new
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Route not found",
                ExceptionCode = ExceptionConvention.NotFound,
            });
        }
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();