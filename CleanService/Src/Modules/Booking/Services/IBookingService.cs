using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace  CleanService.Src.Modules.Booking.Services;

public interface IBookingService
{
    Task<string> CreateBooking(CreateBookingRequestDto createBookingDto);
    
    Task UpdateBooking(Guid id, UpdateBookingRequestDto updateBookingDto);
    
    Task<Pagination<BookingResponseDto>> GetAllBookings(int? page, int? limit);

    Task<BookingResponseDto?> GetBookingById(Guid id);
    
    Task<string?> AssignHelperToBooking(Bookings booking);
    
    Task CreateRefund(CreateRefundRequestDto createRefundRequestDto);
    
    Task UpdateRefund(Guid id, UpdateRefundRequestDto updateRefundRequestDto);
    
    Task<Pagination<RefundResponseDto>> GetAllComplaints(int? page, int? limit);
    
    Task<Pagination<RefundResponseDto>> GetComplaintByCustomerId(string? id,int? page, int? limit);
    
    Task CreateFeedback(CreateFeedbackDto createFeedbackDto);
    
    Task<Pagination<FeedbackResponseDto>> GetFeedbackByCustomerId(string? id,int? page, int? limit);

}

