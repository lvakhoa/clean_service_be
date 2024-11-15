using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;
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
    
    Task UpdateRefund(Guid id, CusUpdateRefundRequestDto cusUpdateRefundRequestDto);
    
    Task<Pagination<CusRefundResponseDto>> GetAllComplaints(int? page, int? limit);
    
    Task<Pagination<CusRefundResponseDto>> GetComplaintByCustomerId(string? id,int? page, int? limit);
    
    Task CreateFeedback(CreateFeedbackDto createFeedbackDto);
    
    Task<Pagination<CusFeedbackResponseDto>> GetFeedbackByCustomerId(string? id,int? page, int? limit);

}

