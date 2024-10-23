

using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;
using CleanService.Src.Modules.Booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Booking;


[Route("[controller]")]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    } 

    [HttpPost]
    public async Task<ActionResult<BookingReturnDto>> CreateBooking([FromBody] CreateBookingDto booking)
    {
        var test = await _bookingService.CreateBooking(booking);
        return test;
    }

    [HttpGet]
    public async Task<ActionResult<BookingReturnDto[]>> GetAllBookings()
    {
        return await _bookingService.GetAllBookings();
    }
}
