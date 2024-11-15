using System.Net;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Booking;

[Route("[controller]")]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateBooking([FromBody] CreateBookingRequestDto createBooking)
    {
        var paymentLink = await _bookingService.CreateBooking(createBooking);
        return CreatedAtAction("CreateBooking", new SuccessResponse()
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Create booking successfully",
            Data = new Dictionary<string, object>()
            {
                { "paymentLink", paymentLink }
            }
        });
    }

    [HttpPatch("update/{id}")]
    public async Task<ActionResult> UpdateBooking(Guid id, [FromBody] UpdateBookingRequestDto updateBooking)
    {
        await _bookingService.UpdateBooking(id, updateBooking);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update booking successfully"
        });
    }

    [HttpGet("all")]
    public async Task<ActionResult<Pagination<BookingResponseDto>>> GetAllBookings(int? page, int? limit)
    {
        var bookings = await _bookingService.GetAllBookings(page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all bookings successfully",
            Data = bookings
        });
    }

    [HttpGet("refund")]
    public async Task<ActionResult<Pagination<BookingResponseDto>>> GetRefundByCustomerId(string? customerId,int? page, int? limit)
    {
        var refunds = await _bookingService.GetComplaintByCustomerId(customerId, page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get refund by customerId successfully",
            Data = refunds
        });
    }
    
    [HttpGet("feedback")]
    public async Task<ActionResult<Pagination<BookingResponseDto>>> GetFeedbackByCustomerId(string? customerId,int? page, int? limit)
    {
        var refunds = await _bookingService.GetFeedbackByCustomerId(customerId, page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get feedback by customerId successfully",
            Data = refunds
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto?>> GetBookingById(Guid id)
    {
        var booking = await _bookingService.GetBookingById(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get booking successfully",
            Data = booking
        });
    }
    
    [HttpPost("refund")]
    public async Task<ActionResult> CreateRefund([FromBody] CreateRefundRequestDto createRefund)
    {
        await _bookingService.CreateRefund(createRefund);
        return CreatedAtAction("CreateRefund", new SuccessResponse()
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Create refund successfully"
        });
    }
    
    [HttpPatch("refund/{id}")]
    public async Task<ActionResult> UpdateRefund(Guid id, [FromBody] CusUpdateRefundRequestDto cusUpdateRefund)
    {
        await _bookingService.UpdateRefund(id, cusUpdateRefund);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update refund successfully"
        });
    }
    
    
    [HttpPost("feedback")]
    public async Task<ActionResult> CreateFeedback([FromBody] CreateFeedbackDto createFeedback)
    {
            await _bookingService.CreateFeedback(createFeedback);

            return CreatedAtAction("CreateFeedback", new SuccessResponse()
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Create feedback successfully"
            });
    }
}