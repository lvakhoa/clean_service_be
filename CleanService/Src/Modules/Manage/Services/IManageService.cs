using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using Pagination.EntityFrameworkCore.Extensions;

namespace  CleanService.Src.Modules.Manage.Services;

public interface IManageService
{
    public Task<Pagination<HelperDetailResponseDto>> GetHelper(int? page, int? limit, UserStatus? userStatus = UserStatus.Active);
    public Task<Pagination<UserResponseDto>> GetCustomer(int? page, int? limit, UserStatus? userStatus = UserStatus.Active);
    public Task<UserResponseDto> GetCustomerById(string id);
    public Task<Pagination<FeedbackResponseDto>> GetFeedbacks(int? page, int? limit);
    public Task<FeedbackResponseDto> GetFeedbackById(Guid id);
    public Task<Pagination<FeedbackResponseDto>> GetFeedbacksOfCurrentCustomer(string userId,int? page, int? limit);
    public Task<Pagination<RefundResponseDto>> GetRefundsOfCurrentCustomer(string userId, int? page, int? limit);
    public Task DeleteFeedback(Guid id);
    public Task<Pagination<RefundResponseDto>> GetRefunds(RefundStatus? status,int? page, int? limit);
    public Task<RefundResponseDto> GetRefundById(Guid id);
    public Task UpdateRefund(Guid id, UpdateRefundRequestDto updateRefundRequestDto);
    public Task DeleteRefund(Guid id);
    public Task<Pagination<RoomPricingResponseDto>> GetRoomPricing(int? page, int? limit, RoomType? roomType, Guid? serviceTypeId);
    public Task CreateRoomPricing(CreateRoomPricingRequestDto createRoomPricingRequestDto);
    public Task UpdateRoomPricing(Guid id,UpdateRoomPricingRequestDto updateRoomPricingRequestDto);
    public Task DeleteRoomPricing(Guid id);
    public Task<Pagination<DurationPricingResponseDto>> GetDurationPrices(int? page, int? limit);
    public Task CreateDurationPrice(CreateDurationPriceRequestDto createDurationPriceRequestDto);
    public Task UpdateDurationPrice(Guid id, UpdateDurationPriceRequestDto updateDurationPriceRequestDto);
    public Task DeleteDurationPrice(Guid id);
    public Task HandleRefund(Guid id, RefundStatus? status);
    public Task UpdateUserInfo(string id, AdminUpdateUserRequestDto  updateUserRequestDto);
    public Task UpdateHelperInfo(string id, AdminUpdateHelperRequestDto updateHelperRequestDto);
}


