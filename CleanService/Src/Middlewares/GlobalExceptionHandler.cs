using System.Net;
using CleanService.Src.Constant;
using CleanService.Src.Helpers;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanService.Src.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await HandleExceptionAsync(httpContext, exception, cancellationToken);
        
        return true;
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ExceptionResponse exceptionResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exceptionResponse.StatusCode;
            await context.Response.WriteAsJsonAsync(exceptionResponse, cancellationToken);
        }
        else
        {
            var response = exception switch
            {
                UniqueConstraintException _ => new ExceptionResponse(HttpStatusCode.BadRequest,
                    "Unique constraint exception occurred.", ExceptionConvention.UniqueConstraintViolation),
                MaxLengthExceededException _ => new ExceptionResponse(HttpStatusCode.BadRequest,
                    "Maximum length exceeded.", ExceptionConvention.MaxLengthViolation),
                KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found.",
                    ExceptionConvention.NotFound),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized.",
                    ExceptionConvention.Unauthorized),
                
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError,
                    "Internal server error. Please retry later.", ExceptionConvention.InternalServerError)
            };

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                _logger.LogError(exception, "An unexpected error occurred.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response, cancellationToken);
        }
    }
}