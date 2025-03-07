using System.Net;
using System.Security.Claims;

using CleanService.Src.Common;
using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Scheduler.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Scheduler;

public class SchedulerController : ApiController
{
    private readonly ISchedulerService _schedulerService;

    public SchedulerController(ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
    }

    // [HttpGet("all")]
    // public async Task<ActionResult<BookingResponseDto[]>> GetScheduledBookings(int? page, int? limit)
    // {
    //     var result = await _schedulerService.GetAllScheduledBookings(page, limit);
    //     return Ok(new SuccessResponse
    //     {
    //         StatusCode = HttpStatusCode.OK,
    //         Message = "Get all bookings successfully",
    //         Data = result
    //     });
    // }

    [HttpGet("current")]
    public async Task<ActionResult<BookingResponseDto[]>> GetCurrentUserBookings()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userType = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (userId == null)
            throw new UnauthorizedAccessException();

        if (userType == "Customer")
        {
            var result = await _schedulerService.QueryScheduledBooking(userId);
            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Get current customer's bookings successfully",
                Data = result
            });
        }
        else if (userType == "Helper")
        {
            // Nếu là Helper, truy vấn booking của người giúp việc
            var result = await _schedulerService.QueryScheduledBooking(null, userId);
            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Get current helper's bookings successfully",
                Data = result
            });
        }
        else
        {
            throw new ForbiddenException("Forbidden");
        }
    }

    [HttpGet()]
    public async Task<ActionResult<BookingResponseDto[]>> GetScheduledBookingByHelperId(string? helperId, string? customerId, int? page, int? limit)
    {
        var result = await _schedulerService.QueryScheduledBooking(customerId, helperId, page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get bookings by helper id successfully",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto>> GetScheduledBookingById(Guid id)
    {
        var booking = await _schedulerService.GetScheduledBookingById(id);
        if (booking == null) return NotFound(new { message = "Booking not found." });
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
            Message = "Cancel booking successfully"
        });
        // TODO: notification to admin
        // TODO: refund to customer
    }
}
