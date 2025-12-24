using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Jobs;

/// <summary>
/// Background job that periodically checks for bookings that should be cancelled
/// and updates their status to Cancelled in the database.
/// </summary>
public class BookingCancellationJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BookingCancellationJob> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // Check every 5 minutes
    private readonly TimeSpan _paymentTimeout = TimeSpan.FromMinutes(16); // Cancel after 16 minutes of non-payment

    public BookingCancellationJob(IServiceProvider serviceProvider, ILogger<BookingCancellationJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BookingCancellationJob is starting.");

        // Wait for the application to be fully started
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessBookingCancellations(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing booking cancellations.");
            }

            // Wait for the next interval
            await Task.Delay(_checkInterval, stoppingToken);
        }

        _logger.LogInformation("BookingCancellationJob is stopping.");
    }

    private async Task ProcessBookingCancellations(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        try
        {
            // Find bookings that are Pending and have unpaid payment status
            var pendingBookings = await unitOfWork.Repository<Bookings, PartialBookings>()
                .GetAllAsync(BookingSpecification.GetPendingBookingsSpec());

            if (!pendingBookings.Any())
            {
                _logger.LogDebug("No pending bookings found to process.");
                return;
            }

            var now = DateTime.UtcNow;
            var cancelledCount = 0;

            foreach (var booking in pendingBookings)
            {
                // Check if payment timeout has elapsed
                var timeSinceCreation = now - booking.CreatedAt;

                if (booking.PaymentStatus == PaymentStatus.Pending &&
                    timeSinceCreation > _paymentTimeout)
                {
                    _logger.LogInformation(
                        "Cancelling booking {BookingId} (OrderId: {OrderId}) - Payment timeout exceeded. Created at: {CreatedAt}, Time elapsed: {TimeElapsed}",
                        booking.Id, booking.OrderId, booking.CreatedAt, timeSinceCreation);

                    booking.Status = BookingStatus.Cancelled;
                    booking.CancellationReason = $"Payment not received within {_paymentTimeout.TotalMinutes} minutes";
                    booking.HelperId = null;
                    booking.UpdatedAt = DateTime.UtcNow;

                    cancelledCount++;
                }
            }

            if (cancelledCount > 0)
            {
                await unitOfWork.SaveChangesAsync();
                _logger.LogInformation(
                    "Successfully cancelled {CancelledCount} booking(s) due to payment timeout.",
                    cancelledCount);
            }
            else
            {
                _logger.LogDebug("No bookings needed to be cancelled in this cycle.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing booking cancellations in database operation.");
            throw;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("BookingCancellationJob is stopping gracefully.");
        await base.StopAsync(cancellationToken);
    }
}

