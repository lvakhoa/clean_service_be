using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using CleanService.Src.Constant;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Infrastructures;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.BlackListed;
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using CleanService.Src.Repositories;
using CleanService.Src.Utils;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Manage.Services;

public class ManageService : IManageService
{
    private readonly IManageUnitOfWork _manageUnitOfWork;
    private readonly IMapper _mapper;

    public ManageService(IManageUnitOfWork manageUnitOfWork, IMapper mapper)
    {
        _manageUnitOfWork = manageUnitOfWork;
        _mapper = mapper;
    }

    public Task<Pagination<HelperDetailResponseDto>> GetDetailHelper(int? page, int? limit, UserStatus? userStatus = UserStatus.Active)
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

    public Task<Pagination<RefundResponseDto>> GetRefunds(RefundStatus? status,int? page, int? limit)
    {
        Expression<Func<Refunds, bool>> predicate = status != null? entity => entity.Status == status : entity => true;
        
        var refunds = _manageUnitOfWork.RefundRepository.Find(predicate,
            order: entity => entity.CreatedAt,false,null, null,
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

    public async Task UpdateRefund(Guid id, UpdateRefundRequestDto updateRefundRequestDto)
    {
        var refund = await _manageUnitOfWork.RefundRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (refund == null)
            throw new KeyNotFoundException("Refund not found");
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

    public Task<Pagination<RoomPricingResponseDto>> GetRoomPricing(int? page, int? limit)
    {
        var roomPricings = _manageUnitOfWork.RoomPricingRepository.Find(
            entity => true,
            entity => entity.CreatedAt,false,
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
        
        if(Enum.IsDefined(typeof(RoomType), roomPricing.RoomType) == false)
            throw new KeyNotFoundException("Room Type not found");
        
        if(roomPricing.RoomCount is < 0 or > 5 )
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Room Count must be in [0,5] range",
                ExceptionConvention.ValidationFailed);
        
        await _manageUnitOfWork.RoomPricingRepository.AddAsync(roomPricing);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task CreateBlackListedDto(CreateBlackListedDto createBlackListedDto)
    {
        var blacklisted = _mapper.Map<BlacklistedUsers>(createBlackListedDto);
        
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
        if(updateRoomPricingRequestDto.RoomType != null && Enum.IsDefined(typeof(RoomType), updateRoomPricingRequestDto.RoomType) == false)
            throw new KeyNotFoundException("Room Type not found");
        if(updateRoomPricingRequestDto.RoomCount != null && updateRoomPricingRequestDto.RoomCount is < 0 or > 5 )
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Room Count must be in [0,5] range",
            ExceptionConvention.ValidationFailed);
        
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
        if(createDurationPriceRequestDto.DurationHours < 1)
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Duration Hours must be greater than 0",
                ExceptionConvention.ValidationFailed);
        if(createDurationPriceRequestDto.PriceMultiplier < 0)
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Price Multiplier must be greater than 0",
                ExceptionConvention.ValidationFailed);
        
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