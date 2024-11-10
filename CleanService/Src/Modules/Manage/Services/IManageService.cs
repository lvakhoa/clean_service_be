using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
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
    public Task<Pagination<RoomPricingResponseDto>> GetRoomPricing(int? page, int? limit);
    public Task CreateRoomPricing(CreateRoomPricingRequestDto createRoomPricingRequestDto);
    public Task UpdateRoomPricing(Guid id,UpdateRoomPricingRequestDto updateRoomPricingRequestDto);
    public Task DeleteRoomPricing(Guid id);
    public Task<Pagination<DurationPricingResponseDto>> GetDurationPrices(int? page, int? limit);
    public Task CreateDurationPrice(CreateDurationPriceRequestDto createDurationPriceRequestDto);
    public Task UpdateDurationPrice(Guid id, UpdateDurationPriceRequestDto updateDurationPriceRequestDto);
    public Task DeleteDurationPrice(Guid id);
    
    
}


