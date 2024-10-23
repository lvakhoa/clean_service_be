using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;
using CleanService.Src.Modules.Booking.Repositories;

namespace CleanService.Src.Modules.Booking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository) 
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<BookingReturnDto>> GetAllBooking(bool? isProcessed = null)
        {
            
            var bookingList = await _bookingRepository.GetAllBookings(isProcessed);
            return bookingList.ToList();
        }
    }
}