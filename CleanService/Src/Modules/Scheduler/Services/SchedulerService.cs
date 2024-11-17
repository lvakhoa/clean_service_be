using System.Linq.Expressions;
using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Infrastructures;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Repositories;
using Pagination.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Scheduler.Services;

public class SchedulerService : ISchedulerService
{
    private readonly IBookingUnitOfWork _bookingUnitOfWork;
    private readonly IMapper _mapper;

    public SchedulerService(IBookingUnitOfWork bookingUnitOfWork, IMapper mapper)
    {
        _bookingUnitOfWork = bookingUnitOfWork;
        _mapper = mapper;
    }
    public async Task<Pagination<BookingResponseDto>> GetAllScheduledBookings(int? page, int? limit)
    {
        var bookings =  _bookingUnitOfWork.BookingRepository.GetAll(page, limit, 
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalBookings = await _bookingUnitOfWork.BookingRepository.CountAsync();
        var bookingsDto = _mapper.Map<BookingResponseDto[]>(bookings);
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalBookings;
        
        return new Pagination<BookingResponseDto>(bookingsDto, totalBookings,
            currentPage,
            currentLimit);
    }

    public async Task<BookingResponseDto?> GetScheduledBookingById(Guid id)
    {
        var booking = await  _bookingUnitOfWork.BookingRepository.FindOneAsync(entity => entity.Id == id)
            .ContinueWith(task => _mapper.Map<BookingResponseDto>(task.Result));
        if(booking == null)
            throw new KeyNotFoundException("Booking not found");
        
        //var bookingDto = _mapper.Map<BookingResponseDto>(booking);
        return booking;
    }

    public async Task<Pagination<BookingResponseDto>> QueryScheduledBooking(string? customerId = null, string? helperId = null, int? page = null, int? limit = null)
    {
        
        var booking =  _bookingUnitOfWork.BookingRepository.Find(
            entity => (customerId == null || entity.CustomerId == customerId) && 
                      (helperId == null || entity.HelperId == helperId),
            x => x.ScheduledStartTime,
            true,
            page,
            limit,
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalCount = await _bookingUnitOfWork.BookingRepository.CountAsync(x => (customerId == null || x.CustomerId == customerId) && 
            (helperId == null || x.HelperId == helperId));

        var bookingDtos = _mapper.Map<BookingResponseDto[]>(booking);
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalCount;

        return new Pagination<BookingResponseDto>
        (
            bookingDtos,
            totalCount,
            currentPage,
            currentLimit
        );
    }

    public async Task CancelScheduledBooking(Guid bookingId)
    {
        var booking = await _bookingUnitOfWork.BookingRepository.FindOneAsync(entity => entity.Id == bookingId, new FindOptions
        {
            IsIgnoreAutoIncludes = true
        });
        if (booking == null)
            throw new KeyNotFoundException("Booking not found");
        
        _bookingUnitOfWork.BookingRepository.Detach(booking);

        // var updateBooking = _mapper.Map<PartialBookings>(new UpdateBookingRequestDto()
        // {
        //     Status = BookingStatus.Cancelled
        // });

        var updateBooking = new PartialBookings()
        {
            Status = BookingStatus.Cancelled,
        };
        _bookingUnitOfWork.BookingRepository.Update(updateBooking, booking);

        await _bookingUnitOfWork.SaveChangesAsync();
    }
}