

using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
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
    public async Task<ActionResult<string>> CreateBooking([FromBody] CreateBookingDto booking)
    {
        return await _bookingService.CreateBooking(booking);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<BookingReturnDto?>> UpdateBooking(Guid id,[FromBody] UpdateBookingDto booking)
    {
        return await _bookingService.UpdateBooking(id, booking);
    }
    
    [HttpGet]
    public async Task<ActionResult<BookingReturnDto[]>> GetAllBookings()
    {
        return await _bookingService.GetAllBookings();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingReturnDto?>> GetBookingById(Guid id)
    {
        return await _bookingService.GetBookingById(id);
    }
}
