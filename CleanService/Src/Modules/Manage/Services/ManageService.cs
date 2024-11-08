using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Infrastructures;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
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
        var complaintDto = _mapper.Map<ComplaintResponseDto[]>(complaints);
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalComplaints;
        
        return Task.FromResult(new Pagination<ComplaintResponseDto>(complaintDto, complaints.ToList().Count, currentPage, currentLimit));
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
    
    
}