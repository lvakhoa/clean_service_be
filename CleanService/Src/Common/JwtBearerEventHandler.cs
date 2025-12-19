using System.Security.Claims;

using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models.Domains;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CleanService.Src.Common;

public class JwtBearerEventHandler : JwtBearerEvents
{
    public override async Task TokenValidated(TokenValidatedContext context)
    {
        if (context.Principal is null)
        {
            context.Fail(new SecurityTokenValidationException());
            return;
        }

        var userId = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            context.Fail(new SecurityTokenValidationException());
            return;
        }

        var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
        var userSpec = UserSpecification.GetUserByIdSpec(userId);
        var user = await unitOfWork.Repository<Users, PartialUsers>().GetFirstAsync(userSpec);
        if (user == null)
        {
            context.Fail(new SecurityTokenValidationException());
            return;
        }

        context.Success();
    }

    public override async Task Challenge(JwtBearerChallengeContext context)
    {
        context.HandleResponse();
        var errorMessage = "Authentication failed. Please check your credentials.";

        if (context.AuthenticateFailure != null)
        {
            errorMessage = context.AuthenticateFailure switch
            {
                SecurityTokenExpiredException => "Your session has expired. Please log in again.",
                SecurityTokenInvalidSignatureException => "Invalid authentication token.",
                SecurityTokenValidationException => "Invalid authentication token.",
                ArgumentException when context.AuthenticateFailure.Message.Contains("IDX") =>
                    "Token validation failed.",
                _ => context.AuthenticateFailure.Message == "User is not active"
                    ? "Your account is not active. Please contact an administrator."
                    : "Authentication failed. Please check your credentials."
            };
        }

        var result = JsonSerializer.Serialize(new[] { errorMessage });
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }

    public override async Task Forbidden(ForbiddenContext context)
    {
        var result = JsonSerializer.Serialize(new[] { "Forbidden" });
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
}
