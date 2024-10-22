using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Auth.Repositories;

public class BookingRepository : IBookingRepository 
{
    private readonly CleanServiceContext _dbContext;

    public BookingRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookingReturnDto?> GetBookingById(string id)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        return booking is not null
            ? new BookingReturnDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                HelperId = booking.HelperId,
                BookingDate = booking.BookingDate,
                BookingTime = booking.BookingTime,
                BookingStatus = booking.BookingStatus
            }
            : null;
    }

    public async Task<BookingReturnDto> CreateBooking(CreateBookingDto createBooking)
    {
        var bookingEntity = await _dbContext.Bookings.AddAsync(new Bookings
        {
            Id = createBooking.Id,
            UserId = createBooking.UserId,
            HelperId = createBooking.HelperId,
            BookingDate = createBooking.BookingDate,
            BookingTime = createBooking.BookingTime,
            BookingStatus = createBooking.BookingStatus
        });

        await _dbContext.SaveChangesAsync();

        return new BookingReturnDto
        {
            Id = bookingEntity.Entity.Id,
            UserId = bookingEntity.Entity.UserId,
            HelperId = bookingEntity.Entity.HelperId,
            BookingDate = bookingEntity.Entity.BookingDate,
            BookingTime = bookingEntity.Entity.BookingTime,
            BookingStatus = bookingEntity.Entity.BookingStatus
        };
    }

    public async Task<BookingReturnDto?> UpdateBooking(string id, UpdateBookingDto updateBooking)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        if (booking is null)
        {
            return null;
        }

        booking.BookingDate = updateBooking.BookingDate;
        booking.BookingTime = updateBooking.BookingTime;
        booking.BookingStatus = updateBooking.BookingStatus;

        await _dbContext.SaveChangesAsync();

        return new BookingReturnDto
        {
            Id = booking.Id,
            UserId = booking.UserId,
            HelperId = booking.HelperId,
            BookingDate = booking.BookingDate,
            BookingTime = booking.BookingTime,
            BookingStatus = booking.BookingStatus
        };
    }

    public async Task<BookingReturnDto[]> GetAllBookings()
    {
        var bookings = await _dbContext.Bookings.ToArrayAsync();

        return bookings.Select(booking => new BookingReturnDto
        {
            Id = booking.Id,
            UserId = booking.UserId,
            HelperId = booking.HelperId,
            BookingDate = booking.BookingDate,
            BookingTime = booking.BookingTime,
            BookingStatus = booking.BookingStatus
        }).ToArray();
    }
}