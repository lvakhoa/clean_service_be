using System.Linq.Expressions;

using AutoMapper;

using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Configurations;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

using Microsoft.EntityFrameworkCore;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Scheduler.Services;

public class SchedulerService : ISchedulerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SchedulerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Pagination<BookingResponseDto>> GetAllScheduledBookings(int? page, int? limit)
    {
        var bookingSpec = BookingSpecification.GetPagedBookingsSpec(page, limit);
        var bookings = await _unitOfWork.Repository<Bookings, PartialBookings>().GetAllAsync(bookingSpec);
        var totalBookings = await _unitOfWork.Repository<Bookings, PartialBookings>().CountAsync();
        var bookingsDto = _mapper.Map<BookingResponseDto[]>(bookings);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalBookings;

        return new Pagination<BookingResponseDto>(bookingsDto, totalBookings,
            currentPage,
            currentLimit);
    }

    public async Task<BookingResponseDto?> GetScheduledBookingById(Guid id)
    {
        return await _unitOfWork.Repository<Bookings, PartialBookings>().GetFirstOrThrowAsync(BookingSpecification.GetBookingByIdSpec(id))
            .ContinueWith(task => _mapper.Map<BookingResponseDto>(task.Result));
    }

    public async Task<Pagination<BookingResponseDto>> QueryScheduledBooking(string? customerId = null, string? helperId = null, int? page = null, int? limit = null)
    {
        var bookingSpec = BookingSpecification.GetBookingByUserIdSpec(customerId ?? helperId, customerId != null ? UserType.Customer : UserType.Helper);
        bookingSpec.ApplyOrderByDescending(x => x.ScheduledStartTime);
        if (page.HasValue && limit.HasValue)
        {
            bookingSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>().GetAllAsync(bookingSpec);
        var totalCount = await _unitOfWork.Repository<Bookings, PartialBookings>().CountAsync(BookingSpecification.GetBookingByUserIdSpec(customerId ?? helperId, customerId != null ? UserType.Customer : UserType.Helper));

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
        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>().GetFirstOrThrowAsync(BookingSpecification.GetBookingByIdSpec(bookingId));

        await _unitOfWork.Repository<Bookings, PartialBookings>().Detach(booking);

        var updateBooking = new PartialBookings()
        {
            Status = BookingStatus.Cancelled,
        };
        await _unitOfWork.Repository<Bookings, PartialBookings>().UpdateAsync(updateBooking, booking);

        await _unitOfWork.SaveChangesAsync();
    }
}
