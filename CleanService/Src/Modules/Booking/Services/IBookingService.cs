using CleanService.Src.Modules.Booking.DTOs;

namespace CleanService.Src.Modules.Booking.Services
{
    public interface IBookingService
    {
        Task<List<BookingReturnDto>> GetAllClasss();
    }
}