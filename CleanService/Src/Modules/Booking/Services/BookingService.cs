using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Infrastructures;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingUnitOfWork _bookingUnitOfWork;
    private readonly IMapper _mapper;

    public BookingService(IBookingUnitOfWork bookingUnitOfWork, IMapper mapper)
    {
        _bookingUnitOfWork = bookingUnitOfWork;
        _mapper = mapper;
    }

    public async Task CreateBooking(CreateBookingRequestDto createBookingDto)
    {
        var booking = _mapper.Map<Bookings>(createBookingDto);
        
        var customer = await _bookingUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == booking.CustomerId);
        if (customer == null)
            throw new KeyNotFoundException("Customer not found");
        var serviceType =
            await _bookingUnitOfWork.ServiceTypeRepository.FindOneAsync(entity => entity.Id == booking.ServiceTypeId);
        if(serviceType == null)
            throw new KeyNotFoundException("Service type not found");

        /*
         Calculate the pricing for booking based on:
         - Room Type (Bedroom, Bathroom, Kitchen, Living Room)
         - Duration Price
         */
        var basePrice = serviceType.BasePrice;
        var bedroomPricing =
            (await _bookingUnitOfWork.RoomPricingRepository.FindOneAsync(entity =>
                entity.RoomType == RoomType.Bedroom && entity.RoomCount == booking.BookingDetails.BedroomCount))
            ?.AdditionalPrice ?? 0;
        var bathroomPricing =
            (await _bookingUnitOfWork.RoomPricingRepository.FindOneAsync(entity =>
                entity.RoomType == RoomType.Bathroom && entity.RoomCount == booking.BookingDetails.BathroomCount))
            ?.AdditionalPrice ?? 0;
        var kitchenPricing =
            (await _bookingUnitOfWork.RoomPricingRepository.FindOneAsync(entity =>
                entity.RoomType == RoomType.Kitchen && entity.RoomCount == booking.BookingDetails.KitchenCount))
            ?.AdditionalPrice ?? 0;
        var livingRoomPricing =
            (await _bookingUnitOfWork.RoomPricingRepository.FindOneAsync(entity =>
                entity.RoomType == RoomType.LivingRoom && entity.RoomCount == booking.BookingDetails.LivingRoomCount))
            ?.AdditionalPrice ?? 0;
        var durationPricing = (await _bookingUnitOfWork.DurationPriceRepository
                .FindOneAsync(entity => entity.Id == booking.BookingDetails.DurationPriceId))
            ?.PriceMultiplier ?? 0;

        var totalPrice = basePrice + bedroomPricing + bathroomPricing +
                         kitchenPricing + livingRoomPricing +
                         basePrice * durationPricing;
        booking.TotalPrice = totalPrice;

        await _bookingUnitOfWork.BookingRepository.AddAsync(booking);

        await _bookingUnitOfWork.ContractRepository.AddAsync(new Contracts()
        {
            BookingId = booking.Id,
            Content = "Clean at " + booking.Location
        });

        booking.HelperId = await AssignHelperToBooking(booking);
        
        if (booking.HelperId == null)
        {
            booking.Status = BookingStatus.Cancelled;
            // Send notification to customer that no helper is available
        }
        else
        {
            booking.Status = BookingStatus.Confirmed;
        }
        
        await _bookingUnitOfWork.SaveChangesAsync();
        
    }

    public async Task UpdateBooking(Guid id, UpdateBookingRequestDto updateBookingDto)
    {
        var booking = await _bookingUnitOfWork.BookingRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsIgnoreAutoIncludes = true
        });
        if (booking == null)
            throw new KeyNotFoundException("Booking not found");
        _bookingUnitOfWork.BookingRepository.Detach(booking);

        var updateBooking = _mapper.Map<PartialBookings>(updateBookingDto);
        _bookingUnitOfWork.BookingRepository.Update(updateBooking, booking);

        await _bookingUnitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<BookingResponseDto>> GetAllBookings(int? page, int? limit)
    {
        var bookings = _bookingUnitOfWork.BookingRepository.GetAll(page, limit,
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalBookings = await _bookingUnitOfWork.BookingRepository.CountAsync();

        var bookingDtos = _mapper.Map<BookingResponseDto[]>(bookings);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalBookings;

        return new Pagination<BookingResponseDto>(bookingDtos, totalBookings,
            currentPage,
            currentLimit);
    }

    public async Task<BookingResponseDto?> GetBookingById(Guid id)
    {
        var booking = await _bookingUnitOfWork.BookingRepository.FindOneAsync(entity => entity.Id == id)
            .ContinueWith(task => _mapper.Map<BookingResponseDto>(task.Result));
        if(booking == null)
            throw new KeyNotFoundException("Booking not found");
        
        var bookingDto = _mapper.Map<BookingResponseDto>(booking);
        return bookingDto;
    }
    
    public async Task<string?> AssignHelperToBooking(Bookings booking)
    {
        // Get the customer who made the booking
        var customer = booking.Customer;

        // Get the list of all helpers
        var allHelpers = _bookingUnitOfWork.UserRepository.Find(x => x.UserType == UserType.Helper).ToList();
        var availableHelpers = new List<Users>();

        int? minJobTaken = null;
        var relevantStatuses = new List<BookingStatus> { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress };

        foreach (var helper in allHelpers)
        {
            // Get the list of bookings for the current helper
            var helperBookings = await _bookingUnitOfWork.BookingRepository.GetBookingByUserId(relevantStatuses, helper.Id, UserType.Helper);
            var jobCount = helperBookings.Count(x => x.Status == BookingStatus.Completed || x.Status == BookingStatus.Confirmed || x.Status == BookingStatus.InProgress);

            // Check if the helper is available during the requested time
            bool isAvailable = helperBookings.All(x =>
                x.Status != BookingStatus.Confirmed ||
                x.ScheduledEndTime <= booking.ScheduledStartTime ||
                x.ScheduledStartTime >= booking.ScheduledEndTime
            );

            if (isAvailable)
            {
                minJobTaken = minJobTaken == null ? jobCount : Math.Min(minJobTaken.Value, jobCount);
                availableHelpers.Add(helper);
            }
        }

        // Filter out helpers who are blacklisted by the customer
        availableHelpers = availableHelpers.Where(x => !customer.BlacklistedByUsers.Any(y => y.UserId == x.Id)).ToList();

        // Filter out helpers who do not offer the required service
        availableHelpers = availableHelpers
            .Where(x => x.Helper.ServicesOffered != null && x.Helper.ServicesOffered.Contains(booking.ServiceTypeId))
            .ToList();

        if (!availableHelpers.Any()) return null;

        // Find the most suitable helpers with the least number of jobs taken
        var mostSuitableHelpers = new List<Users>();
        foreach (var helper in availableHelpers)
        {
            var helperBookings = await _bookingUnitOfWork.BookingRepository.GetBookingByUserId(relevantStatuses, helper.Id, UserType.Helper);
            var jobCount = helperBookings.Count(x => x.Status == BookingStatus.Completed || x.Status == BookingStatus.Confirmed || x.Status == BookingStatus.InProgress);

            if (jobCount == minJobTaken)
            {
                mostSuitableHelpers.Add(helper);
            }
        }

        // Randomly choose a helper from the most suitable helpers
        var random = new Random();
        var selectedHelper = mostSuitableHelpers[random.Next(mostSuitableHelpers.Count)];

        return selectedHelper.Id;
    }
    
}