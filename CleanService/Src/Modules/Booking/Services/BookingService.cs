using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;
using CleanService.Src.Modules.Booking.Repositories;
using CleanService.Src.Modules.Service.Services;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IServiceService _serviceService;

    public BookingService(IBookingRepository bookingRepository, IServiceService serviceService)
    {
        _bookingRepository = bookingRepository;
        _serviceService = serviceService;
    }

    public async Task<BookingReturnDto> CreateBooking(CreateBookingDto createBookingDto)
    {
        var price = _serviceService.GetPriceById(createBookingDto.ServiceId);
        var booking = new Bookings
        {
            Id = Guid.NewGuid(),
            CustomerId = createBookingDto.CustomerId,
            HelperId = createBookingDto.HelperId,
            ServiceId = createBookingDto.ServiceId,
            Location = createBookingDto.Location,
            StartTime = createBookingDto.StartTime,
            EndTime = createBookingDto.EndTime,
            Price = price,
            PaymentMethod = createBookingDto.PaymentMethod,
            Rating = null,
            Feedback = null,
            CancellationReason = null,
        };
        return await _bookingRepository.CreateBooking(booking);;
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

