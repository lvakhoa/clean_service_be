using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Infrastructures;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using CleanService.Src.Repositories;
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

    public async Task<Pagination<HelperDetailResponseDto>> GetDetailHelper(int? page, int? limit, UserStatus? userStatus = UserStatus.Active)
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

        return new Pagination<HelperDetailResponseDto>(helperDto, totalHelpers,
            currentPage,
            currentLimit);
    }

    public Task<Pagination<FeedbackResponseDto>> GetFeedbacks(int? page, int? limit)
    {
        throw new NotImplementedException();
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

    public Task<Pagination<BookingResponseDto>> GetBookings(int? page, int? limit)
    {
        throw new NotImplementedException();
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
        if (roomPricings.ToList()[0].ServiceType == null) 
            throw new KeyNotFoundException("Service Type not found");
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

    public Task UpdateRoomPricing(Guid id, UpdateRoomPricingRequestDto updateRoomPricingRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoomPricing(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Pagination<DurationPricingResponseDto>> GetDurationPrices(int? page, int? limit)
    {
        throw new NotImplementedException();
    }

    public Task CreateDurationPrice(CreateDurationPriceRequestDto createDurationPriceRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDurationPrice(Guid id, UpdateDurationPriceRequestDto updateDurationPriceRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDurationPrice(Guid id)
    {
        throw new NotImplementedException();
    }
}