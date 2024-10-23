using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Booking.Repositories;

public class BookingRepository : IBookingRepository 
{
    private readonly CleanServiceContext _dbContext;

    public BookingRepository(CleanServiceContext dbContext)
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
                Status = booking.Status,
                CancellationReason = booking.CancellationReason,
                Price = booking.Price,
                PaymentMethod = booking.PaymentMethod,
                Rating = booking.Rating,
                Feedback = booking.Feedback,
            }
            : null;
    }

    public async Task<BookingReturnDto> CreateBooking(CreateBookingDto createBooking)
    {
        var bookingEntity = await _dbContext.Bookings.AddAsync(new Bookings
        {
            Id = Guid.NewGuid(),
            CustomerId = createBooking.CustomerId,
            HelperId = createBooking.HelperId,
            ServiceId = createBooking.ServiceId,
            Location = createBooking.Location,
            StartTime = createBooking.StartTime,
            EndTime = createBooking.EndTime,
            Price = createBooking.Price,
            PaymentMethod = createBooking.PaymentMethod,
            Rating = null,
            Feedback = null,
            CancellationReason = null,
        });

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
            Status = bookingEntity.Entity.Status,
            CancellationReason = bookingEntity.Entity.CancellationReason,
            Price = bookingEntity.Entity.Price,
            PaymentMethod = bookingEntity.Entity.PaymentMethod,
            Rating = bookingEntity.Entity.Rating,
            Feedback = bookingEntity.Entity.Feedback,
        };
    }
    
    // public async Task<BookingReturnDto> CreateBooking(CreateBookingDto createBooking)
    // {
    //     var bookingEntity = await _dbContext.Bookings.AddAsync(new Bookings
    //     {
    //         Id = Guid.NewGuid(),
    //         CustomerId = createBooking.CustomerId,
    //         HelperId = createBooking.HelperId,
    //         ServiceId = createBooking.ServiceId,
    //         Location = createBooking.Location,
    //         StartTime = createBooking.StartTime,
    //         EndTime = createBooking.EndTime,
    //         Price = createBooking.Price,
    //         PaymentMethod = createBooking.PaymentMethod,
    //         Rating = null,
    //         Feedback = null,
    //         CancellationReason = null,
    //     });
    //     
    //     await _dbContext.SaveChangesAsync();
    //
    //     return new BookingReturnDto
    //     {
    //         Id = bookingEntity.Entity.Id,
    //         CustomerId = bookingEntity.Entity.CustomerId,
    //         HelperId = bookingEntity.Entity.HelperId,
    //         ServiceId = bookingEntity.Entity.ServiceId,
    //         Location = bookingEntity.Entity.Location,
    //         StartTime = bookingEntity.Entity.StartTime,
    //         EndTime = bookingEntity.Entity.EndTime,
    //         Status = bookingEntity.Entity.Status,
    //         CancellationReason = bookingEntity.Entity.CancellationReason,
    //         Price = bookingEntity.Entity.Price,
    //         PaymentMethod = bookingEntity.Entity.PaymentMethod,
    //         Rating = bookingEntity.Entity.Rating,
    //         Feedback = bookingEntity.Entity.Feedback,
    //     };
    // }
    
    public async Task<BookingReturnDto?> UpdateBooking(Guid id, UpdateBookingDto updateBooking)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
    
        if (booking is null)
        {
            return null;
        }
    
        booking.HelperId = updateBooking.HelperId;
        booking.Location = updateBooking.Location;
        booking.EndTime = updateBooking.EndTime;
        booking.Status = (BookingStatus)updateBooking.Status;
        booking.CancellationReason = updateBooking.CancellationReason;
        booking.Price = updateBooking.Price;
        booking.PaymentMethod = updateBooking.PaymentMethod;
        booking.Feedback = updateBooking.Feedback;
        booking.Rating = updateBooking.Rating;
    
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
            Status = booking.Status,
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
            Status = booking.Status,
            CancellationReason = booking.CancellationReason,
            Price = booking.Price,
            PaymentMethod = booking.PaymentMethod,
            Rating = booking.Rating,
            Feedback = booking.Feedback,
        }).ToArray();
    }
}