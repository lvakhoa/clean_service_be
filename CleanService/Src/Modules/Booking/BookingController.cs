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
        await _bookingService.CreateBooking(createBooking);
        
        return CreatedAtAction("CreateBooking", new SuccessResponse()
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Create booking successfully"
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
    
    
    
    // Complaint API
    [HttpPost("complaint/create")]
    public async Task<ActionResult> CreateComplaint([FromBody] CreateComplaintDto createComplaint)
    {
        try
        {
            await _bookingService.CreateComplaint(createComplaint);
            return CreatedAtAction("CreateComplaint", new SuccessResponse()
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Create complaint successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine( e.Message);
            throw;
        }
        
    }
    
    [HttpPatch("complaint/{id}")]
    public async Task<ActionResult> UpdateComplaint(Guid id, [FromBody] UpdateComplaintDto updateComplaints)
    {
        await _bookingService.UpdateComplaint(id, updateComplaints);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update complaint successfully"
        });
    }

    [HttpGet("complaint/{customerId}")]
    public async Task<ActionResult<Pagination<ComplaintResponseDto>>> GetComplaintByCustomerId(string customerId, int? page, int? limit)
    {
        var result = await _bookingService.GetComplaintByCustomerId(customerId, page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get complaints successfully",
            Data = result
        });
    }
}