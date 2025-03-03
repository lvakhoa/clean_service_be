using CleanService.Src.Models;

namespace CleanService.Src.Middlewares;

public class TransactionMiddleware
{
    private readonly ILogger<TransactionMiddleware> _logger;

    private readonly RequestDelegate _next;

    public TransactionMiddleware(RequestDelegate next, ILogger<TransactionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, CleanServiceContext databaseContext)
    {
        await using var transaction = await databaseContext.Database.BeginTransactionAsync();

        try
        {
            await _next(context);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }
}
