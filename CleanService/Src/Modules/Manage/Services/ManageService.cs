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
            order: entity => entity.User.FullName, page, limit,
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

    public Task<Pagination<ComplaintResponseDto>> GetComplaints(ComplaintStatus? status,int? page, int? limit)
    {
        Expression<Func<Complaints, bool>> predicate = status != null? entity => entity.Status == status : entity => true;
        
        var complaints = _manageUnitOfWork.ComplaintRepository.Find(predicate,
            order: entity => entity.CreatedAt,null, null,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        
        var totalComplaints = complaints.ToList().Count;
        var complaintDto = _mapper.Map<ComplaintResponseDto[]>(complaints.ToList());
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalComplaints;
        
        return Task.FromResult(new Pagination<ComplaintResponseDto>(complaintDto, totalComplaints, currentPage, currentLimit));
    }

    public async Task UpdateComplaint(Guid id, UpdateComplaintRequestDto updateComplaintRequestDto)
    {
        var complaint = await _manageUnitOfWork.ComplaintRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (complaint == null)
            throw new KeyNotFoundException("Complaint not found");
        _manageUnitOfWork.ComplaintRepository.Detach(complaint);
        
        var complaintEntity = _mapper.Map<PartialComplaints>(updateComplaintRequestDto);
        
        _manageUnitOfWork.ComplaintRepository.Update(complaintEntity, complaint);
        
        await _manageUnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteComplaint(Guid id)
    {
        var complaint = await _manageUnitOfWork.ComplaintRepository.FindOneAsync(entity => entity.Id == id);

        if (complaint == null)
        {
            throw new KeyNotFoundException("Complaint not found");
        }
        
        _manageUnitOfWork.ComplaintRepository.Delete(complaint);
        
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
            entity => entity.CreatedAt,
            null,
            null,
            new FindOptions
            {
                IsAsNoTracking = true,
                Includes = new Expression<Func<RoomPricing, object>>[]
                {
                    x => x.ServiceType
                }
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