using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;
using CleanService.Src.Modules.Booking.Repositories;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingReturnDto> CreateBooking(CreateBookingDto createBookingDto)
    {
        //return await _bookingRepository.CreateBooking(createBookingDto);
        return await _bookingRepository.CreateBooking(createBookingDto);;
    }

    public async Task<BookingReturnDto> UpdateBooking(Guid id, UpdateBookingDto updateBookingDto)
    {
        var booking = await _bookingRepository.GetBookingById(id);
        if(booking == null)
            throw new KeyNotFoundException("Booking not found");
        return await _bookingRepository.UpdateBooking(id, updateBookingDto);
    }

    public Task<BookingReturnDto[]> GetAllBookings()
    {
        return _bookingRepository.GetAllBookings();
    }

    public Task<BookingReturnDto?> GetBookingById(Guid id)
    {
        return _bookingRepository.GetBookingById(id);
    }
}

