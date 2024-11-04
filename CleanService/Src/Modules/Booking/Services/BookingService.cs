using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Repositories;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Repositories;
using CleanService.Src.Modules.Contract.Mapping.DTOs;
using CleanService.Src.Modules.Contract.Repositories;
using CleanService.Src.Modules.ServicePricing.Repositories;
using CleanService.Src.Modules.ServiceType.Repositories;
using System.Collections.Generic;
using System;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IServiceTypeRepository _serviceTypeRepository;
    private readonly IServicePricingRepository _servicePricingRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly IAuthRepository _authRepository;

    public BookingService(IBookingRepository bookingRepository, IServiceTypeRepository serviceTypeRepository,
        IServicePricingRepository servicePricingRepository,
        IContractRepository contractRepository, IMapper mapper, IAuthRepository authRepository)
    {
        _bookingRepository = bookingRepository;
        _serviceTypeRepository = serviceTypeRepository;
        _servicePricingRepository = servicePricingRepository;
        _contractRepository = contractRepository;
        _mapper = mapper;
        _authRepository = authRepository;
    }

    public async Task<string> CreateBooking(CreateBookingDto createBookingDto)
    {
        var booking = _mapper.Map<Bookings>(createBookingDto);

        /*
         Calculate the pricing for booking based on:
         - Room Type (Bedroom, Bathroom, Kitchen, Living Room)
         - Duration Price
         */
        var selectedService = await _serviceTypeRepository.GetServiceById(booking.ServiceTypeId);
        var basePrice = selectedService?.BasePrice ?? 0;
        var bedroomPricing =
            (await _servicePricingRepository.GetUniqueRoomPricingType(RoomType.Bedroom,
                booking.BookingDetails.BedroomCount))?.AdditionalPrice ?? 0;
        var bathroomPricing =
            (await _servicePricingRepository.GetUniqueRoomPricingType(RoomType.Bathroom,
                booking.BookingDetails.BathroomCount))?.AdditionalPrice ?? 0;
        var kitchenPricing =
            (await _servicePricingRepository.GetUniqueRoomPricingType(RoomType.Kitchen,
                booking.BookingDetails.KitchenCount))?.AdditionalPrice ?? 0;
        var livingRoomPricing =
            (await _servicePricingRepository.GetUniqueRoomPricingType(RoomType.LivingRoom,
                booking.BookingDetails.LivingRoomCount))?.AdditionalPrice ?? 0;
        var durationPricing =
            (await _servicePricingRepository.GetDurationPriceTypeById(booking.BookingDetails.DurationPriceId))
            ?.PriceMultiplier ?? 1;

        var totalPrice = basePrice + bedroomPricing + bathroomPricing +
                         kitchenPricing + livingRoomPricing +
                         basePrice * durationPricing;
        booking.TotalPrice = totalPrice;

        var bookingEntity = await _bookingRepository.CreateBooking(booking);

        await _contractRepository.CreateContract(new CreateContractDto
        {
            BookingId = booking.Id,
            Content = "Clean at " + booking.Location
        });
        return bookingEntity.Id.ToString();
    }

    public async Task<BookingReturnDto?> UpdateBooking(Guid id, UpdateBookingDto updateBookingDto)
    {
        var booking = await _bookingRepository.GetBookingById(id);
        if (booking == null)
            throw new KeyNotFoundException("Booking not found");

        var updateBooking = _mapper.Map<PartialBookings>(updateBookingDto);
        var bookingEntity = await _bookingRepository.UpdateBooking(id, updateBooking);
        var bookingDto = _mapper.Map<BookingReturnDto>(bookingEntity);

        return bookingDto;
    }

    public async Task<BookingReturnDto[]> GetAllBookings()
    {
        var bookings = await _bookingRepository.GetAllBookings();
        var bookingsDto = _mapper.Map<BookingReturnDto[]>(bookings);
        return bookingsDto;
    }

    public async Task<BookingReturnDto?> GetBookingById(Guid id)
    {
        var booking = await _bookingRepository.GetBookingById(id);
        var bookingDto = _mapper.Map<BookingReturnDto>(booking);
        return bookingDto;
    }

    public async Task<string?> AssignHelperToBooking(Guid id)
    {
        // Lấy booking cần gán helper
        var booking = await _bookingRepository.GetBookingById(id);
        if (booking == null)
            throw new KeyNotFoundException("Booking not found"); ;

        // Lấy customer đặt booking
        var customer = booking.Customer;

        // Lấy danh sách helper rảnh rỗi trong khoảng thời gian
        var helperList = await _authRepository.GetUsers(UserType.Helper);
        var availableHelpers = new List<Users>();

        foreach (var helper in helperList.Results)
        {
            // Lấy danh sách booking của helper hiện tại
            var statuses = new List<BookingStatus> { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress };

            var helperBookings = await _bookingRepository.GetBookingByUserId(statuses, helper.Id, UserType.Helper);

            // Kiểm tra xem helper có rảnh rỗi trong khoảng thời gian yêu cầu không
            bool isAvailable = helperBookings.All(x =>
                x.Status != BookingStatus.Confirmed ||
                x.ScheduledEndTime <= booking.ScheduledStartTime ||
                x.ScheduledStartTime >= booking.ScheduledEndTime
            );

            if (isAvailable)
            {
                availableHelpers.Add(helper);
            }
        }
        //

        //Blacklist của Customer
        availableHelpers = availableHelpers.Where(x => !customer.BlacklistedByUsers.Any(y => y.UserId == x.Id)).ToList();
        //

        //Helper có cung cấp dịch vụ đó không
        availableHelpers = availableHelpers
            .Where(x => x.Helper.ServicesOffered != null && x.Helper.ServicesOffered.Contains(booking.ServiceTypeId))
            .ToList();
        //

        //Helper có nghỉ vào ngày đó không 
        //

        // Gán ngẫu nhiên helper vào booking
        if (availableHelpers == null) return null;

        Random random = new Random();

        int index = random.Next(availableHelpers.Count);

        await _bookingRepository.UpdateBooking(booking.Id, new PartialBookings
        {
            HelperId = availableHelpers[index].Id
        });

        return availableHelpers[index].Id;
    }
}