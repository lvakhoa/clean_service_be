using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace  CleanService.Src.Modules.Booking.Services;

public interface IBookingService
{
    Task CreateBooking(CreateBookingRequestDto createBookingDto);
    
    Task UpdateBooking(Guid id, UpdateBookingRequestDto updateBookingDto);
    
    Task<Pagination<BookingResponseDto>> GetAllBookings(int? page, int? limit);

    Task<BookingResponseDto?> GetBookingById(Guid id);
    
    Task<string?> AssignHelperToBooking(Bookings booking);
    
    Task CreateComplaint(CreateComplaintDto createComplaintDto);
    
    Task UpdateComplaint(Guid id, UpdateComplaintDto updateComplaintDto);
    
    Task<Pagination<ComplaintResponseDto>> GetAllComplaints(int? page, int? limit);
    
    Task<Pagination<ComplaintResponseDto>> GetComplaintByCustomerId(string id,int? page, int? limit);
}

