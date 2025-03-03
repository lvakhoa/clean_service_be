using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

using AutoMapper;

using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
// using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.BlackListed;
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using CleanService.Src.Modules.Storage.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Mvc;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Manage.Services;

public class ManageService : IManageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;

    public ManageService(IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public async Task<Pagination<HelperDetailResponseDto>> GetHelper(int? page, int? limit,
        UserStatus? userStatus = UserStatus.Active)
    {
        var helperSpec = HelperSpecification.GetHelperByStatusSpec(userStatus);
        helperSpec.ApplyOrderBy(entity => entity.User.FullName);
        if (page != null && limit != null)
        {
            helperSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var helpers = await _unitOfWork.Repository<Helpers, PartialHelper>().GetAllAsync(helperSpec);
        var totalHelpers = helpers.Count;

        var helperDto = _mapper.Map<HelperDetailResponseDto[]>(helpers);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalHelpers;

        return new Pagination<HelperDetailResponseDto>(helperDto, totalHelpers, currentPage, currentLimit);
    }

    public async Task<HelperDetailResponseDto> GetHelperById(string id)
    {
        return await _unitOfWork.Repository<Helpers, PartialHelper>()
            .GetFirstOrThrowAsync(HelperSpecification.GetHelperByIdSpec(id))
            .ContinueWith(x => _mapper.Map<HelperDetailResponseDto>(x.Result));
    }

    public async Task<Pagination<UserResponseDto>> GetCustomer(int? page, int? limit,
        UserStatus? userStatus = UserStatus.Active)
    {
        var customerSpec = UserSpecification.GetUserByStatusOrTypeSpec(UserType.Customer, userStatus);
        customerSpec.ApplyOrderBy(entity => entity.FullName);
        if (page != null && limit != null)
        {
            customerSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var customers = await _unitOfWork.Repository<Users, PartialUsers>().GetAllAsync(customerSpec);
        var totalCustomers = customers.Count;

        var helperDto = _mapper.Map<UserResponseDto[]>(customers);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalCustomers;

        return new Pagination<UserResponseDto>(helperDto, totalCustomers, currentPage, currentLimit);
    }

    public async Task<UserResponseDto> GetCustomerById(string id)
    {
        return await _unitOfWork.Repository<Users, PartialUsers>()
            .GetFirstOrThrowAsync(UserSpecification.GetUserByIdSpec(id))
            .ContinueWith(x => _mapper.Map<UserResponseDto>(x.Result));
    }

    public async Task<Pagination<FeedbackResponseDto>> GetFeedbacks(int? page, int? limit)
    {
        var feedbacks = await _unitOfWork.Repository<Feedbacks, PartialFeedback>().GetAllAsync()
            .ContinueWith(x => _mapper.Map<FeedbackResponseDto[]>(x.Result));

        var totalFeedbacks = feedbacks.Length;
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalFeedbacks;

        return new Pagination<FeedbackResponseDto>(feedbacks, totalFeedbacks, currentPage, currentLimit);
    }

    public async Task<FeedbackResponseDto> GetFeedbackById(Guid id)
    {
        return await _unitOfWork.Repository<Feedbacks, PartialFeedback>()
            .GetFirstOrThrowAsync(FeedbackSpecification.GetFeedbackByIdSpec(id))
            .ContinueWith(x => _mapper.Map<FeedbackResponseDto>(x.Result));
    }

    public async Task<Pagination<FeedbackResponseDto>> GetFeedbacksOfCurrentCustomer(string userId, int? page,
        int? limit)
    {
        var feedbackSpec = FeedbackSpecification.GetFeedbackByCustomerIdSpec(userId);
        feedbackSpec.ApplyOrderBy(entity => entity.CreatedAt);
        if (page != null && limit != null)
        {
            feedbackSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var feedbacks = await _unitOfWork.Repository<Feedbacks, PartialFeedback>().GetAllAsync(feedbackSpec)
            .ContinueWith(x => _mapper.Map<FeedbackResponseDto[]>(x.Result));

        var totalFeedbacks = feedbacks.Length;

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalFeedbacks;

        return new Pagination<FeedbackResponseDto>(feedbacks, totalFeedbacks, currentPage, currentLimit);
    }

    public async Task<Pagination<RefundResponseDto>> GetRefundsOfCurrentCustomer(string userId, int? page, int? limit)
    {
        var refundSpec = RefundSpecification.GetRefundByCustomerIdSpec(userId);
        refundSpec.ApplyOrderBy(x => x.CreatedAt);
        if (page != null && limit != null)
        {
            refundSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var refunds = await _unitOfWork.Repository<Refunds, PartialRefunds>().GetAllAsync(refundSpec)
            .ContinueWith(x => _mapper.Map<RefundResponseDto[]>(x.Result));

        var totalRefunds = refunds.Length;
        var refundDto = _mapper.Map<RefundResponseDto[]>(refunds.ToList());

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalRefunds;

        return new Pagination<RefundResponseDto>(refundDto, totalRefunds, currentPage, currentLimit);
    }

    public async Task DeleteFeedback(Guid id)
    {
        var feedback = await _unitOfWork.Repository<Feedbacks, PartialFeedback>()
            .GetFirstOrThrowAsync(FeedbackSpecification.GetFeedbackByIdSpec(id));

        await _unitOfWork.Repository<Feedbacks, PartialFeedback>().DeleteAsync(feedback);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<RefundResponseDto>> GetRefunds(RefundStatus? status, int? page, int? limit)
    {
        var refundSpec = RefundSpecification.GetRefundByStatusSpec(status);
        refundSpec.ApplyOrderBy(x => x.CreatedAt);
        if (page != null && limit != null)
        {
            refundSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var refunds = await _unitOfWork.Repository<Refunds, PartialRefunds>().GetAllAsync(refundSpec)
            .ContinueWith(x => _mapper.Map<RefundResponseDto[]>(x.Result));

        var totalRefunds = refunds.Length;

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalRefunds;

        return new Pagination<RefundResponseDto>(refunds, totalRefunds, currentPage, currentLimit);
    }

    public async Task<RefundResponseDto> GetRefundById(Guid id)
    {
        return await _unitOfWork.Repository<Refunds, PartialRefunds>()
            .GetFirstOrThrowAsync(RefundSpecification.GetRefundByIdSpec(id))
            .ContinueWith(x => _mapper.Map<RefundResponseDto>(x.Result));
    }

    public async Task UpdateRefund(Guid id, UpdateRefundRequestDto updateRefundRequestDto)
    {
        var refund = await _unitOfWork.Repository<Refunds, PartialRefunds>()
            .GetFirstOrThrowAsync(RefundSpecification.GetRefundByIdSpec(id));
        await _unitOfWork.Repository<Bookings, PartialBookings>()
            .GetFirstOrThrowAsync(BookingSpecification.GetBookingByIdSpec(refund.BookingId));
        await _unitOfWork.Repository<Refunds, PartialRefunds>().Detach(refund);

        var refundEntity = _mapper.Map<PartialRefunds>(updateRefundRequestDto);

        await _unitOfWork.Repository<Refunds, PartialRefunds>().UpdateAsync(refundEntity, refund);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task HandleRefund(Guid id, RefundStatus? status)
    {
        var refund = await _unitOfWork.Repository<Refunds, PartialRefunds>()
            .GetFirstOrThrowAsync(RefundSpecification.GetRefundByIdSpec(id));
        await _unitOfWork.Repository<Refunds, PartialRefunds>().Detach(refund);

        var updateRefund = new PartialRefunds() { Status = status, ResolvedAt = DateTime.Now };

        if (status == RefundStatus.Refunded)
        {
            if (refund.Booking.HelperId == null)
            {
                throw new NotFoundException("Helper not found", ExceptionConvention.NotFound);
            }

            var helper = await _unitOfWork.Repository<Users, PartialUsers>()
                .GetFirstOrThrowAsync(UserSpecification.GetUserByIdSpec(refund.Booking.HelperId));
            await _unitOfWork.Repository<Users, PartialUsers>().Detach(helper);
            await _unitOfWork.Repository<Users, PartialUsers>().UpdateAsync(
                new PartialUsers { NumberOfViolation = helper.NumberOfViolation + 1 }, helper);

            // await CreateBlackListedDto(new CreateBlackListedDto()
            // {
            //     UserId = refund.Booking.HelperId,
            //     Reason = "Bad service",
            //     BlacklistedBy = refund.Booking.CustomerId,
            //     IsPermanent = false
            // });
        }

        await _unitOfWork.Repository<Refunds, PartialRefunds>().UpdateAsync(updateRefund, refund);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateUserInfo(string id, AdminUpdateUserRequestDto adminUpdateUserRequestDto)
    {
        var user = await _unitOfWork.Repository<Users, PartialUsers>()
            .GetFirstOrThrowAsync(UserSpecification.GetUserByIdSpec(id));
        await _unitOfWork.Repository<Users, PartialUsers>().Detach(user);

        if (user.ProfilePicture != null && user.ProfilePicture.Length > 0 &&
            adminUpdateUserRequestDto.ProfilePicture != null)
        {
            await _storageService.DeleteFileAsync(user.ProfilePicture);
        }

        if (user.IdentityCard != null && user.IdentityCard.Length > 0 && adminUpdateUserRequestDto.IdCard != null)
        {
            await _storageService.DeleteFileAsync(user.IdentityCard);
        }

        var userEntity = _mapper.Map<PartialUsers>(adminUpdateUserRequestDto);

        await _unitOfWork.Repository<Users, PartialUsers>().UpdateAsync(userEntity, user);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateHelperInfo(string id, AdminUpdateHelperRequestDto adminUpdateHelperRequestDto)
    {
        var helper = await _unitOfWork.Repository<Helpers, PartialHelper>()
            .GetFirstOrThrowAsync(HelperSpecification.GetHelperByIdSpec(id));
        await _unitOfWork.Repository<Helpers, PartialHelper>().Detach(helper);

        if (helper.ResumeUploaded != null)
        {
            await _storageService.DeleteFileAsync(helper.ResumeUploaded);
        }

        var helperEntity = _mapper.Map<PartialHelper>(adminUpdateHelperRequestDto);

        await _unitOfWork.Repository<Helpers, PartialHelper>().UpdateAsync(helperEntity, helper);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRefund(Guid id)
    {
        var refund = await _unitOfWork.Repository<Refunds, PartialRefunds>()
            .GetFirstOrThrowAsync(RefundSpecification.GetRefundByIdSpec(id));

        await _unitOfWork.Repository<Refunds, PartialRefunds>().DeleteAsync(refund);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<RoomPricingResponseDto>> GetRoomPricing(int? page, int? limit, RoomType? roomType,
        Guid? serviceTypeId)
    {
        Expression<Func<RoomPricing, bool>> predicate = entity => true;
        var roomPricingSpec =
            RoomPricingSpecification.GetRoomPricingByRoomTypeOrServiceTypeSpec(roomType, serviceTypeId);
        roomPricingSpec.ApplyOrderBy(entity => entity.RoomType);
        if (page != null && limit != null)
        {
            roomPricingSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var roomPricings = await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>().GetAllAsync(roomPricingSpec)
            .ContinueWith(x => _mapper.Map<RoomPricingResponseDto[]>(x.Result));

        var totalRoomPricings = roomPricings.Length;

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalRoomPricings;

        return new Pagination<RoomPricingResponseDto>(roomPricings, totalRoomPricings, currentPage, currentLimit);
    }

    public async Task CreateRoomPricing(CreateRoomPricingRequestDto createRoomPricingRequestDto)
    {
        var roomPricing = _mapper.Map<RoomPricing>(createRoomPricingRequestDto);


        await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>()
            .GetFirstOrThrowAsync(ServiceTypeSpecification.GetServiceTypeByIdSpec(roomPricing.ServiceTypeId));

        await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>().AddAsync(roomPricing);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateBlackListed(CreateBlackListedDto createBlackListedDto)
    {
        var blacklisted = _mapper.Map<BlacklistedUsers>(createBlackListedDto);

        await _unitOfWork.Repository<Users, PartialUsers>()
            .GetFirstOrThrowAsync(UserSpecification.GetUserByIdSpec(blacklisted.UserId));

        await _unitOfWork.Repository<Users, PartialUsers>()
            .GetFirstOrThrowAsync(UserSpecification.GetUserByIdSpec(blacklisted.BlacklistedBy));

        await _unitOfWork.Repository<BlacklistedUsers, PartialBlacklistedUsers>().AddAsync(blacklisted);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRoomPricing(Guid id, UpdateRoomPricingRequestDto updateRoomPricingRequestDto)
    {
        var roomPricing = await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>()
            .GetFirstOrThrowAsync(RoomPricingSpecification.GetRoomPricingByIdSpec(id));


        if (updateRoomPricingRequestDto.ServiceTypeId.HasValue)
        {
            await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().GetFirstOrThrowAsync(
                ServiceTypeSpecification.GetServiceTypeByIdSpec(updateRoomPricingRequestDto.ServiceTypeId.Value));
        }

        await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>().Detach(roomPricing);

        var roomPricingEntity = _mapper.Map<PartialRoomPricing>(updateRoomPricingRequestDto);

        await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>().UpdateAsync(roomPricingEntity, roomPricing);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRoomPricing(Guid id)
    {
        var roomPricing = await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>()
            .GetFirstOrThrowAsync(RoomPricingSpecification.GetRoomPricingByIdSpec(id));

        await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>().DeleteAsync(roomPricing);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<DurationPricingResponseDto>> GetDurationPrices(int? page, int? limit)
    {
        var durationPriceSpec = DurationPriceSpecification.GetDurationPriceOrderByDurationHoursSpec();
        if (page != null && limit != null)
        {
            durationPriceSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var durationPrices = await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>()
            .GetAllAsync(durationPriceSpec).ContinueWith(x => _mapper.Map<DurationPricingResponseDto[]>(x.Result));

        var totalDurationPrices = durationPrices.Length;

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalDurationPrices;

        return new Pagination<DurationPricingResponseDto>(durationPrices, totalDurationPrices, currentPage,
            currentLimit);
    }

    public async Task CreateDurationPrice(CreateDurationPriceRequestDto createDurationPriceRequestDto)
    {
        var durationPrice = _mapper.Map<DurationPrice>(createDurationPriceRequestDto);

        var serviceType = await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>()
            .GetFirstOrThrowAsync(ServiceTypeSpecification.GetServiceTypeByIdSpec(durationPrice.ServiceTypeId));

        await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>().AddAsync(durationPrice);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateDurationPrice(Guid id, UpdateDurationPriceRequestDto updateDurationPriceRequestDto)
    {
        var durationPrice = await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>()
            .GetFirstOrThrowAsync(DurationPriceSpecification.GetDurationPriceByIdSpec(id));

        if (updateDurationPriceRequestDto.ServiceTypeId.HasValue)
        {
            await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().GetFirstOrThrowAsync(
                ServiceTypeSpecification.GetServiceTypeByIdSpec(updateDurationPriceRequestDto.ServiceTypeId.Value));
        }

        if (updateDurationPriceRequestDto.DurationHours != null && updateDurationPriceRequestDto.DurationHours < 1)
            throw new UnprocessableRequestException("Duration Hours must be greater than 0",
                exceptionCode: ExceptionConvention.ValidationFailed);

        if (updateDurationPriceRequestDto.PriceMultiplier != null && updateDurationPriceRequestDto.PriceMultiplier < 0)
            throw new UnprocessableRequestException("Price Multiplier must be greater than 0",
                exceptionCode: ExceptionConvention.ValidationFailed);

        await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>().Detach(durationPrice);

        var durationPriceEntity = _mapper.Map<PartialDurationPrice>(updateDurationPriceRequestDto);

        await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>()
            .UpdateAsync(durationPriceEntity, durationPrice);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteDurationPrice(Guid id)
    {
        var durationPrice = await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>()
            .GetFirstOrThrowAsync(DurationPriceSpecification.GetDurationPriceByIdSpec(id));

        await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>().DeleteAsync(durationPrice);

        await _unitOfWork.SaveChangesAsync();
    }
}
