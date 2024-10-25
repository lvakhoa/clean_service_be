using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;
using CleanService.Src.Modules.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Booking.Repositories;

public class BookingRepository : IBookingRepository 
{
    private readonly CleanServiceContext _dbContext;

    public BookingRepository(CleanServiceContext dbContext, IServiceService serviceService)
    {
        _dbContext = dbContext;
    }

    public async Task<BookingReturnDto?> GetBookingById(Guid id)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        
        return booking is not null
            ? new BookingReturnDto
            {
                Id = booking.Id,
                CustomerId = booking.CustomerId,
                HelperId = booking.HelperId,
                ServiceId = booking.ServiceId,
                Location = booking.Location,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Status = nameof(booking.Status),
                CancellationReason = booking.CancellationReason,
                Price = booking.Price,
                PaymentMethod = booking.PaymentMethod,
                Rating = booking.Rating,
                Feedback = booking.Feedback,
            }
            : null;
    }

    public async Task<BookingReturnDto> CreateBooking(Bookings createBooking)
    {
        var bookingEntity = await _dbContext.Bookings.AddAsync(createBooking);

        // var contractEntity = await _dbContext.Contracts.AddAsync(new Contracts
        // {
        //     BookingId = bookingEntity.Entity.Id,
        //     Content = "Clean at " + bookingEntity.Entity.Location
        // }); 

        await _dbContext.SaveChangesAsync();
        
        return new BookingReturnDto
        {
            Id = bookingEntity.Entity.Id,
            CustomerId = bookingEntity.Entity.CustomerId,
            HelperId = bookingEntity.Entity.HelperId,
            ServiceId = bookingEntity.Entity.ServiceId,
            Location = bookingEntity.Entity.Location,
            StartTime = bookingEntity.Entity.StartTime,
            EndTime = bookingEntity.Entity.EndTime,
            //Status = Enum.GetName(typeof(BookingStatus),bookingEntity.Entity.Status),
            Status = bookingEntity.Entity.Status.ToString(),
            CancellationReason = bookingEntity.Entity.CancellationReason,
            Price = bookingEntity.Entity.Price,
            PaymentMethod = bookingEntity.Entity.PaymentMethod,
            Rating = bookingEntity.Entity.Rating,
            Feedback = bookingEntity.Entity.Feedback,
        };
    }
    
    public async Task<BookingReturnDto?> UpdateBooking(Guid id, UpdateBookingDto updateBooking)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
    
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
        
        booking.HelperId = updateBooking.HelperId ?? booking.HelperId;
        booking.Location = updateBooking.Location ?? booking.Location;
        booking.EndTime = updateBooking.EndTime ?? booking.EndTime;
        booking.Status = updateBooking.Status ?? booking.Status;
        booking.CancellationReason = updateBooking.CancellationReason ?? booking.CancellationReason;
        booking.PaymentMethod = updateBooking.PaymentMethod ?? booking.PaymentMethod;
        booking.Feedback = updateBooking.Feedback ?? booking.Feedback;
        booking.Rating = updateBooking.Rating ?? booking.Rating;

    
        await _dbContext.SaveChangesAsync();
    
        return new BookingReturnDto
        {
            Id = booking.Id,
            CustomerId = booking.CustomerId,
            HelperId = booking.HelperId,
            ServiceId = booking.ServiceId,
            Location = booking.Location,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Status = booking.Status.ToString(),
            CancellationReason = booking.CancellationReason,
            Price = booking.Price,
            PaymentMethod = booking.PaymentMethod,
            Rating = booking.Rating,
            Feedback = booking.Feedback,
        };
    }
    
    public async Task<BookingReturnDto[]> GetAllBookings()
    {
        var bookings = await _dbContext.Bookings.ToArrayAsync();
    
        return bookings.Select(booking => new BookingReturnDto
        {
            Id = booking.Id,
            CustomerId = booking.CustomerId,
            HelperId = booking.HelperId,
            ServiceId = booking.ServiceId,
            Location = booking.Location,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Status = nameof(booking.Status),
            CancellationReason = booking.CancellationReason,
            Price = booking.Price,
            PaymentMethod = booking.PaymentMethod,
            Rating = booking.Rating,
            Feedback = booking.Feedback,
        }).ToArray();
    }
}