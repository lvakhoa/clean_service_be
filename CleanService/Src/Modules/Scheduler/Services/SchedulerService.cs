using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Repositories;

namespace CleanService.Src.Modules.Scheduler.Services;

public class SchedulerService : ISchedulerService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public SchedulerService(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }
    public async Task<BookingReturnDto[]> GetAllScheduledBookings()
    {
        var bookings = await _bookingRepository.GetAllBookings();
        var bookingsDto = _mapper.Map<BookingReturnDto[]>(bookings);
        return bookingsDto;
    }

    public async Task<BookingReturnDto?> GetScheduledBookingById(Guid id)
    {
        var booking = await _bookingRepository.GetBookingById(id);
        if (booking == null) return null;
        var bookingDto = _mapper.Map<BookingReturnDto>(booking);
        return bookingDto;
    }

    public async Task<BookingReturnDto?> CancelScheduledBooking(Guid bookingId)
    {
        var booking = await _bookingRepository.GetBookingById(bookingId);
        if (booking == null)
            throw new KeyNotFoundException("Booking not found");
        var updatedBooking = await _bookingRepository.UpdateBooking(bookingId, new PartialBookings{Status = BookingStatus.Cancelled});
        var bookingDto = _mapper.Map<BookingReturnDto>(updatedBooking);
        
        return bookingDto;
    }
}