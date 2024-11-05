using System.Net;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Scheduler.Services;
using CleanService.Src.Utils;
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
    public async Task<ActionResult<BookingResponseDto[]>> GetScheduledBookings(int? page, int? limit)
    {
        var result = await _schedulerService.GetAllScheduledBookings(page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all bookings successfully",
            Data = result
        });
    }
    
    [HttpGet("helper/{id}")]
    public async Task<ActionResult<BookingResponseDto[]>> GetScheduledBookingByHelperId(string id, int? page, int? limit)
    {
        var result = await _schedulerService.GetScheduledBookingByHelperId(id,page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get bookings by helper id successfully",
            Data = result
        });
    }
    
    [HttpGet("customer/{id}")]
    public async Task<ActionResult<BookingResponseDto[]>> GetScheduledBookingByCustomerId(string id, int? page, int? limit)
    {
        var result = await _schedulerService.GetScheduledBookingByCustomerId(id,page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get bookings by customer id successfully",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto>> GetScheduledBookingById(Guid id)
    {
        var booking = await _schedulerService.GetScheduledBookingById(id);
        if(booking == null) return NotFound(new { message = "Booking not found." }); 
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get booking successfully",
            Data = booking
        });
    }

    

    [HttpPatch("cancel/{id}")]
    public async Task<ActionResult> CancelScheduledBooking(Guid id)
    {
        await _schedulerService.CancelScheduledBooking(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update booking successfully"
        });
        // TODO: notification to admin
        // TODO: refund to customer
    }
}