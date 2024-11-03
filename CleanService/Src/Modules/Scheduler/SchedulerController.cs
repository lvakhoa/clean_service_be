using System.Net;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Scheduler.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Scheduler;

[Route("[controller]")]
public class SchedulerController : Controller
{
    private readonly ISchedulerService _schedulerService;

    public SchedulerController(ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
    }
    
    [HttpGet]
    public async Task<ActionResult<BookingReturnDto[]>> GetScheduledBookings()
    {
        var result = await _schedulerService.GetAllScheduledBookings();
        
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingReturnDto>> GetScheduledBookingById(Guid id)
    {
        var booking = await _schedulerService.GetScheduledBookingById(id);
        if(booking == null) return NotFound(new { message = "Booking not found." }); 
        return Ok(booking);
    }

    [HttpPatch("cancel/{id}")]
    public async Task<ActionResult<BookingReturnDto?>> CancelScheduledBooking(Guid id)
    {
        return await _schedulerService.CancelScheduledBooking(id);
        // TODO: notification to admin
        // TODO: refund to customer
    }
}