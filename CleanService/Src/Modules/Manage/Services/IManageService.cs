using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace  CleanService.Src.Modules.Manage.Services;

public interface IManageService
{
    public Task<Pagination<HelperDetailResponseDto>> GetDetailHelper(int? page, int? limit, UserStatus? userStatus = UserStatus.Active);
    public Task<Pagination<FeedbackResponseDto>> GetFeedbacks(int? page, int? limit);
    public Task<Pagination<ComplaintResponseDto>> GetComplaints(ComplaintStatus? status,int? page, int? limit);
    public Task UpdateComplaint(Guid id, UpdateComplaintRequestDto updateComplaintRequestDto);
    public Task DeleteComplaint(Guid id);
    public Task<Pagination<BookingResponseDto>> GetBookings(int? page, int? limit);
}


