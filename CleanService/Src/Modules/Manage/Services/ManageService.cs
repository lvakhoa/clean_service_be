using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using CleanService.Src.Constant;
using CleanService.Src.Models;
// using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Infrastructures;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.BlackListed;
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using CleanService.Src.Repositories;
using CleanService.Src.Utils;
using Pagination.EntityFrameworkCore.Extensions;
using System.Security.Claims;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Manage.Services;

public class ManageService : IManageService
{
    private readonly IManageUnitOfWork _manageUnitOfWork;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;

    public ManageService(IManageUnitOfWork manageUnitOfWork, IMapper mapper, IStorageService storageService)
    {
        _manageUnitOfWork = manageUnitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public Task<Pagination<HelperDetailResponseDto>> GetHelper(int? page, int? limit, UserStatus? userStatus = UserStatus.Active)
    {
        Expression<Func<Helpers, bool>> predicate = entity => entity.User.Status == userStatus;
        
        var helpers = _manageUnitOfWork.HelperRepository.Find(predicate,
            order: entity => entity.User.FullName,false, page, limit,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        var totalHelpers = helpers.ToList().Count;

        var helperDto = _mapper.Map<HelperDetailResponseDto[]>(helpers);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalHelpers;

        return Task.FromResult(new Pagination<HelperDetailResponseDto>(helperDto, totalHelpers,
            currentPage,
            currentLimit));
    }

    public async Task<HelperDetailResponseDto> GetHelperById(string id)
    {
        var helper = await _manageUnitOfWork.HelperRepository.FindOneAsync(entity => entity.Id == id,
            new FindOptions
            {
                IsAsNoTracking = true
            });

        if (helper == null)
        {
            throw new KeyNotFoundException("Helper not found");
        }
        
        var helperDto = _mapper.Map<HelperDetailResponseDto>(helper);

        return helperDto;
    }

    public Task<Pagination<UserResponseDto>> GetCustomer(int? page, int? limit, UserStatus? userStatus = UserStatus.Active)
    {
        Expression<Func<Users, bool>> predicate = entity => entity.Status == userStatus && entity.UserType == UserType.Customer;
        
        var customers = _manageUnitOfWork.UserRepository.Find(predicate,
            order: entity => entity.FullName,false, page, limit,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        var totalCustomers = customers.ToList().Count;

        var helperDto = _mapper.Map<UserResponseDto[]>(customers);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalCustomers;

        return Task.FromResult(new Pagination<UserResponseDto>(helperDto, totalCustomers,
            currentPage,
            currentLimit));
    }
    
    public async Task<UserResponseDto> GetCustomerById(string id)
    {
        Expression<Func<Users, bool>> predicate = entity => entity.Id == id && entity.UserType == UserType.Customer;
        
        var customer = await _manageUnitOfWork.UserRepository.FindOneAsync(predicate,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        if (customer == null)
            throw new KeyNotFoundException("Customer not found");

        var customerDto = _mapper.Map<UserResponseDto>(customer);

        return customerDto;
    }

    public Task<Pagination<FeedbackResponseDto>> GetFeedbacks(int? page, int? limit)
    {
        var feedbacks = _manageUnitOfWork.FeedbackRepository.GetAll(
            null,
            null,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        
        var totalFeedbacks = feedbacks.ToList().Count;
        var feedbackDto = _mapper.Map<FeedbackResponseDto[]>(feedbacks.ToList());
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalFeedbacks;
        
        return Task.FromResult(new Pagination<FeedbackResponseDto>(feedbackDto, totalFeedbacks, currentPage, currentLimit));
    }

    public async Task<FeedbackResponseDto> GetFeedbackById(Guid id)
    {
        var feedback = await _manageUnitOfWork.FeedbackRepository.FindOneAsync(entity => entity.Id == id,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        
        if (feedback == null)
            throw new KeyNotFoundException("Feedback not found");
        
        return _mapper.Map<FeedbackResponseDto>(feedback);
    }

    public Task<Pagination<FeedbackResponseDto>> GetFeedbacksOfCurrentCustomer( string userId,int? page, int? limit)
    {
        var feedbacks = _manageUnitOfWork.FeedbackRepository.Find(
            entity => entity.Booking.CustomerId == userId,
            order: entity => entity.CreatedAt,false,
            null,
            null,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        
        var totalFeedbacks = feedbacks.ToList().Count;
        var feedbackDto = _mapper.Map<FeedbackResponseDto[]>(feedbacks.ToList());
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalFeedbacks;
        
        return Task.FromResult(new Pagination<FeedbackResponseDto>(feedbackDto, totalFeedbacks, currentPage, currentLimit));
    }
    
    public Task<Pagination<RefundResponseDto>> GetRefundsOfCurrentCustomer(string userId, int? page, int? limit)
    {
        var refunds = _manageUnitOfWork.RefundRepository.Find(
            entity => entity.Booking.CustomerId == userId,
            order: entity => entity.CreatedAt,false,
            null,
            null,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        
        var totalRefunds = refunds.ToList().Count;
        var refundDto = _mapper.Map<RefundResponseDto[]>(refunds.ToList());
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalRefunds;
        
        return Task.FromResult(new Pagination<RefundResponseDto>(refundDto, totalRefunds, currentPage, currentLimit));
    }

    public async Task DeleteFeedback(Guid id)
    {
        var feedback = await _manageUnitOfWork.FeedbackRepository.FindOneAsync(entity => entity.Id == id);

        if (feedback == null)
        {
            throw new KeyNotFoundException("Feedback not found");
        }
        
        _manageUnitOfWork.FeedbackRepository.Delete(feedback);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }
    
    public Task<Pagination<RefundResponseDto>> GetRefunds(RefundStatus? status,int? page, int? limit)
    {
        Expression<Func<Refunds, bool>> predicate = status != null ? entity => entity.Status == status : entity => true;
        
        var refunds = _manageUnitOfWork.RefundRepository.Find(predicate,
            order: entity => entity.CreatedAt,false,null, null,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        
        var totalRefunds = refunds.ToList().Count;
        var refundDto = _mapper.Map<RefundResponseDto[]>(refunds);
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalRefunds;
        
        return Task.FromResult(new Pagination<RefundResponseDto>(refundDto, totalRefunds, currentPage, currentLimit));
    }

    public async Task<RefundResponseDto> GetRefundById(Guid id)
    {
        var refund = await _manageUnitOfWork.RefundRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true
        });
        
        if (refund == null)
            throw new KeyNotFoundException("Refund not found");
        
        return _mapper.Map<RefundResponseDto>(refund);
    }

    public async Task UpdateRefund(Guid id, UpdateRefundRequestDto updateRefundRequestDto)
    {
        var refund = await _manageUnitOfWork.RefundRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (refund == null)
            throw new KeyNotFoundException("Refund not found");
        var booking = await _manageUnitOfWork.BookingRepository.FindOneAsync(entity => entity.Id == refund.BookingId, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (booking == null)
            throw new KeyNotFoundException("Booking not found");
        _manageUnitOfWork.RefundRepository.Detach(refund);
        
        var refundEntity = _mapper.Map<PartialRefunds>(updateRefundRequestDto);
        
        _manageUnitOfWork.RefundRepository.Update(refundEntity, refund);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task HandleRefund(Guid id, RefundStatus? status)
    {
        var refund = await _manageUnitOfWork.RefundRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true
        });
        if (refund == null)
            throw new KeyNotFoundException("Refund not found");
        _manageUnitOfWork.RefundRepository.Detach(refund);

        var updateRefund = new PartialRefunds()
        {
            Status = status,
            ResolvedAt = DateTime.Now
        };
        
        if (status == RefundStatus.Refunded)
        {
            
            var helper = await _manageUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == refund.Booking.HelperId, new FindOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            });
            _manageUnitOfWork.UserRepository.Detach(helper);
            _manageUnitOfWork.UserRepository.Update(new PartialUsers
            {
                NumberOfViolation = helper.NumberOfViolation + 1
            }, helper);

            // await CreateBlackListedDto(new CreateBlackListedDto()
            // {
            //     UserId = refund.Booking.HelperId,
            //     Reason = "Bad service",
            //     BlacklistedBy = refund.Booking.CustomerId,
            //     IsPermanent = false
            // });
        }
        
        _manageUnitOfWork.RefundRepository.Update(updateRefund, refund);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateUserInfo(string id, AdminUpdateUserRequestDto adminUpdateUserRequestDto)
    {
        var user = await _manageUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (user == null)
            throw new KeyNotFoundException("User not found");
        _manageUnitOfWork.UserRepository.Detach(user);
        
        if(user.ProfilePicture != null && user.ProfilePicture.Length > 0 && adminUpdateUserRequestDto.ProfilePicture != null)
        {
            await _storageService.DeleteFileAsync(user.ProfilePicture);
        }

        if (user.IdentityCard != null && user.IdentityCard.Length > 0 && adminUpdateUserRequestDto.IdCard != null)
        {
            await _storageService.DeleteFileAsync(user.IdentityCard);
        }
        
        var userEntity = _mapper.Map<PartialUsers>(adminUpdateUserRequestDto);
        
        _manageUnitOfWork.UserRepository.Update(userEntity, user);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateHelperInfo(string id, AdminUpdateHelperRequestDto adminUpdateHelperRequestDto)
    {
        var helper = await _manageUnitOfWork.HelperRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        
        if (helper == null)
            throw new KeyNotFoundException("Helper not found");
        _manageUnitOfWork.HelperRepository.Detach(helper);
        
        if (helper.ResumeUploaded != null)
        {
            await _storageService.DeleteFileAsync(helper.ResumeUploaded);
        }
        
        var helperEntity = _mapper.Map<PartialHelper>(adminUpdateHelperRequestDto);
        
        _manageUnitOfWork.HelperRepository.Update(helperEntity, helper);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRefund(Guid id)
    {
        var refund = await _manageUnitOfWork.RefundRepository.FindOneAsync(entity => entity.Id == id);

        if (refund == null)
        {
            throw new KeyNotFoundException("Refund not found");
        }
        
        _manageUnitOfWork.RefundRepository.Delete(refund);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public Task<Pagination<RoomPricingResponseDto>> GetRoomPricing(int? page, int? limit, RoomType? roomType, Guid? serviceTypeId)
    {
        Expression<Func<RoomPricing, bool>> predicate = entity => true;
        if (roomType != null && serviceTypeId == null) 
            predicate = entity => entity.RoomType == roomType;
        if (serviceTypeId != null && roomType == null)
            predicate = entity => entity.ServiceTypeId == serviceTypeId;
        if (serviceTypeId != null && roomType != null)
            predicate = entity => entity.ServiceTypeId == serviceTypeId && entity.RoomType == roomType;
        
        
        var roomPricings = _manageUnitOfWork.RoomPricingRepository.Find(
            predicate,
            entity => entity.RoomCount,false,
            null,
            null,
            new FindOptions
            {
                IsAsNoTracking = true,
            });
    
        var totalRoomPricings = roomPricings.ToList().Count;
        var roomPricingDto = _mapper.Map<RoomPricingResponseDto[]>(roomPricings.ToList());

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalRoomPricings;

        return Task.FromResult(new Pagination<RoomPricingResponseDto>(roomPricingDto, totalRoomPricings, currentPage, currentLimit));
    }

    public async Task CreateRoomPricing(CreateRoomPricingRequestDto createRoomPricingRequestDto)
    {
        var roomPricing = _mapper.Map<RoomPricing>(createRoomPricingRequestDto);
        
        var serviceType = await _manageUnitOfWork.ServiceTypeRepository.FindOneAsync(entity => entity.Id == roomPricing.ServiceTypeId);
        if(serviceType == null)
            throw new KeyNotFoundException("Service Type not found");
        
        await _manageUnitOfWork.RoomPricingRepository.AddAsync(roomPricing);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task CreateBlackListed(CreateBlackListedDto createBlackListedDto)
    {
        var blacklisted = _mapper.Map<BlacklistedUsers>(createBlackListedDto);
        
        var user = await _manageUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == blacklisted.UserId);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        var blacklistedBy = await _manageUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == blacklisted.BlacklistedBy);
        if(blacklistedBy == null)
            throw new KeyNotFoundException("Blacklisted By not found");
        
        await _manageUnitOfWork.BlacklistedUserRepository.AddAsync(blacklisted);
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRoomPricing(Guid id, UpdateRoomPricingRequestDto updateRoomPricingRequestDto)
    {
        var roomPricing = await _manageUnitOfWork.RoomPricingRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        
        if (roomPricing == null)
            throw new KeyNotFoundException("Room Pricing not found");
        
        var serviceType = await _manageUnitOfWork.ServiceTypeRepository.FindOneAsync(entity => entity.Id == updateRoomPricingRequestDto.ServiceTypeId);
        if(serviceType == null && updateRoomPricingRequestDto.ServiceTypeId != null)
            throw new KeyNotFoundException("Service Type not found");
        _manageUnitOfWork.RoomPricingRepository.Detach(roomPricing);
        
        var roomPricingEntity = _mapper.Map<PartialRoomPricing>(updateRoomPricingRequestDto);
        
        _manageUnitOfWork.RoomPricingRepository.Update(roomPricingEntity, roomPricing);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRoomPricing(Guid id)
    {
        var roomPricing = await _manageUnitOfWork.RoomPricingRepository.FindOneAsync(entity => entity.Id == id);

        if (roomPricing == null)
        {
            throw new KeyNotFoundException("Room Pricing not found");
        }
        
        _manageUnitOfWork.RoomPricingRepository.Delete(roomPricing);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public Task<Pagination<DurationPricingResponseDto>> GetDurationPrices(int? page, int? limit)
    {
        var durationPrices = _manageUnitOfWork.DurationPriceRepository.Find(
            entity => true,
            entity => entity.CreatedAt,false,
            null,
            null,
            new FindOptions
            {
                IsAsNoTracking = true,
            });
    
        var totalDurationPrices = durationPrices.ToList().Count;
        var durationPriceDto = _mapper.Map<DurationPricingResponseDto[]>(durationPrices.ToList());

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalDurationPrices;

        return Task.FromResult(new Pagination<DurationPricingResponseDto>(durationPriceDto, totalDurationPrices, currentPage, currentLimit));
    }

    public async Task CreateDurationPrice(CreateDurationPriceRequestDto createDurationPriceRequestDto)
    {
        var durationPrice = _mapper.Map<DurationPrice>(createDurationPriceRequestDto);
        
        var serviceType = await _manageUnitOfWork.ServiceTypeRepository.FindOneAsync(entity => entity.Id == durationPrice.ServiceTypeId);
        if(serviceType == null)
            throw new KeyNotFoundException("Service Type not found");
        
        await _manageUnitOfWork.DurationPriceRepository.AddAsync(durationPrice);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateDurationPrice(Guid id, UpdateDurationPriceRequestDto updateDurationPriceRequestDto)
    {
        var durationPrice = await _manageUnitOfWork.DurationPriceRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        
        if (durationPrice == null)
            throw new KeyNotFoundException("Room Pricing not found");
        
        var serviceType = await _manageUnitOfWork.ServiceTypeRepository.FindOneAsync(entity => entity.Id == updateDurationPriceRequestDto.ServiceTypeId);
        if(serviceType == null )
            throw new KeyNotFoundException("Service Type not found");
        
        if(updateDurationPriceRequestDto.DurationHours != null && updateDurationPriceRequestDto.DurationHours < 1)
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Duration Hours must be greater than 0",
                ExceptionConvention.ValidationFailed);
        
        if(updateDurationPriceRequestDto.PriceMultiplier != null && updateDurationPriceRequestDto.PriceMultiplier < 0)
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Price Multiplier must be greater than 0",
                ExceptionConvention.ValidationFailed);
        
        _manageUnitOfWork.DurationPriceRepository.Detach(durationPrice);
        
        var durationPriceEntity = _mapper.Map<PartialDurationPrice>(updateDurationPriceRequestDto);
        
        _manageUnitOfWork.DurationPriceRepository.Update(durationPriceEntity, durationPrice);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteDurationPrice(Guid id)
    {
        var durationPrice = await _manageUnitOfWork.DurationPriceRepository.FindOneAsync(entity => entity.Id == id);

        if (durationPrice == null)
        {
            throw new KeyNotFoundException("Room Pricing not found");
        }
        
        _manageUnitOfWork.DurationPriceRepository.Delete(durationPrice);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }
}