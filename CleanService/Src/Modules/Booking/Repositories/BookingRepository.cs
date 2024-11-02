using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Booking.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly CleanServiceContext _dbContext;

    public BookingRepository(CleanServiceContext dbContext, IServiceTypeService serviceTypeService)
    {
        _dbContext = dbContext;
    }

    public async Task<Bookings?> GetBookingById(Guid id)
    {
        return await _dbContext.Bookings
            .Include(x => x.Customer)
            .Include(x => x.Helper)
            .Include(x => x.ServiceType)
            .Include(x => x.ServiceType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Bookings> CreateBooking(Bookings createBooking)
    {
        var booking = await _dbContext.Bookings.AddAsync(createBooking);

        await _dbContext.SaveChangesAsync();

        return booking.Entity;
    }

    public async Task<Bookings?> UpdateBooking(Guid id, PartialBookings updateBooking)
    {
        var booking = await _dbContext.Bookings
            .Include(x => x.Customer)
            .Include(x => x.Helper)
            .Include(x => x.ServiceType)
            .Include(x => x.ServiceType)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (booking is null)
        {
            return null;
        }

        // if(updateBooking.HelperId is not null)
        //     booking.HelperId = updateBooking.HelperId;
        // if(updateBooking.Location is not null)
        //     booking.Location = updateBooking.Location;
        // if(updateBooking.EndTime is not null)
        //     booking.EndTime = updateBooking.EndTime;
        // if(updateBooking.Status is not null)
        //     booking.Status = (BookingStatus)updateBooking.Status;
        // if(updateBooking.CancellationReason is not null)
        //     booking.CancellationReason = updateBooking.CancellationReason;
        // if(updateBooking.PaymentMethod is not null)
        //     booking.PaymentMethod = updateBooking.PaymentMethod;
        // if(updateBooking.Rating is not null)
        //     booking.Feedback = updateBooking.Feedback;
        // if(updateBooking.CancellationReason is not null)
        //     booking.Rating = updateBooking.Rating;


        await _dbContext.SaveChangesAsync();

        return booking;
    }

    public async Task<Bookings[]> GetAllBookings()
    {
        return await _dbContext.Bookings
            .Include(x => x.Customer)
            .Include(x => x.Helper)
            .Include(x => x.ServiceType)
            .Include(x => x.ServiceType)
            .ToArrayAsync();
    }
}