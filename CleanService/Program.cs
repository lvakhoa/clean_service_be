using System.Net;
using System.Security.Claims;
using System.Text.Json.Serialization;

using CleanService.Src.Configs;
using CleanService.Src.Constant;
using CleanService.Src.Database;
using CleanService.Src.Exceptions;
using CleanService.Src.Middlewares;
using CleanService.Src.Models;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfig(builder.Configuration);

var origins = Environment.GetEnvironmentVariable("ORIGINS")?.Split(",") ?? new[] { "http://localhost:3000" };

const string allowPolicy = "AllowCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowPolicy,
        policy => { policy.WithOrigins(origins).AllowCredentials().AllowAnyHeader().AllowAnyMethod(); });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthPolicy.IsAdmin,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString()); });

    options.AddPolicy(AuthPolicy.IsHelper,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Helper.ToString()); });

    options.AddPolicy(AuthPolicy.IsCustomer,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Customer.ToString()); });

    options.AddPolicy(AuthPolicy.IsAdminOrHelper,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString(), UserType.Helper.ToString()); });

    options.AddPolicy(AuthPolicy.IsAdminOrCustomer,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString(), UserType.Customer.ToString()); });

    options.AddPolicy(AuthPolicy.IsHelperOrCustomer,
        policy => { policy.RequireClaim(ClaimTypes.Role, UserType.Customer.ToString(), UserType.Helper.ToString()); });

    options.AddPolicy(AuthPolicy.IsAdminOrHelperOrCustomer,
        policy =>
        {
            policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString(), UserType.Customer.ToString(),
                UserType.Helper.ToString());
        });
});

builder.Services.AddAppDependency(builder.Configuration);

Cloudinary cloudinary = new Cloudinary(builder.Configuration["CLOUDINARY_URL"]);

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cleaning Service V1"); });

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors(allowPolicy);

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api"))
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.MapControllers();

app.Run();
