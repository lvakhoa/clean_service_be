using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Infrastructures;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Payment.Services;
using CleanService.Src.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingUnitOfWork _bookingUnitOfWork;
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public BookingService(IBookingUnitOfWork bookingUnitOfWork, IPaymentService paymentService, IMapper mapper)
    {
        _bookingUnitOfWork = bookingUnitOfWork;
        _paymentService = paymentService;
        _mapper = mapper;
    }

    public async Task<string> CreateBooking(CreateBookingRequestDto createBookingDto)
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

        var totalBookings = await _bookingUnitOfWork.BookingRepository.CountAsync();
        booking.OrderId = totalBookings + 1000;

        await _bookingUnitOfWork.BookingRepository.AddAsync(booking);

        await _bookingUnitOfWork.BookingContractRepository.AddAsync(new BookingContracts()
        {
            BookingId = booking.Id,
            Content = createBookingDto.ContractContent
        });

        var paymentLink = await _paymentService.CreatePaymentLink(booking);

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
        
        return paymentLink;
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

    public async Task<Pagination<ComplaintResponseDto>> GetAllComplaints(int? page, int? limit)
    {
        var complaints = _bookingUnitOfWork.ComplaintRepository.GetAll(page, limit,
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalComplaints = await _bookingUnitOfWork.ComplaintRepository.CountAsync();
        
        var complaintDtos = _mapper.Map<ComplaintResponseDto[]>(complaints);
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalComplaints;
        
        return new Pagination<ComplaintResponseDto>(
            complaintDtos,
            totalComplaints,
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
            var jobCount = helperBookings.Count(x => x.Status is BookingStatus.Completed or BookingStatus.Confirmed or BookingStatus.InProgress);

            // Check if the helper is available during the requested time
            var isAvailable = helperBookings.All(x =>
                !relevantStatuses.Contains(x.Status) ||
                x.ScheduledEndTime <= booking.ScheduledStartTime ||
                x.ScheduledStartTime >= booking.ScheduledEndTime
            );
            
            // Check the helper with the least job taken this month
            if (!isAvailable) continue;
            if (minJobTaken == null || jobCount < minJobTaken)
            {
                minJobTaken = jobCount;
                availableHelpers.Clear();
                availableHelpers.Add(helper);
            }
            else if (jobCount == minJobTaken)
            {
                availableHelpers.Add(helper);
            }
        }

        // Filter out helpers who are blacklisted by the customer
        availableHelpers = availableHelpers.Where(x => !customer.BlacklistedByUsers.Any(y => y.UserId == x.Id)).ToList();

        // Filter out helpers who do not offer the required service
        availableHelpers = availableHelpers
            .Where(x => x.Helper?.ServicesOffered != null && x.Helper.ServicesOffered.Contains(booking.ServiceTypeId))
            .ToList();

        if (!availableHelpers.Any()) return null;

        // Find the most suitable helpers with the least number of jobs taken

        // Randomly choose a helper from the most suitable helpers
        var random = new Random();
        var selectedHelper = availableHelpers[random.Next(availableHelpers.Count)];

        return selectedHelper.Id;
    }

    public async Task CreateComplaint(CreateComplaintDto createComplaintDto)
    {
        if (createComplaintDto == null)
        {
            throw new ArgumentNullException(nameof(createComplaintDto), "Complaint data cannot be null.");
        }
        var complaint = _mapper.Map<Complaints>(createComplaintDto);
        
        var booking = await _bookingUnitOfWork.BookingRepository.FindOneAsync(entity => entity.Id == createComplaintDto.BookingId);
        if(booking == null)
            throw new KeyNotFoundException("Booking not found");
        var reportedBy = await _bookingUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == createComplaintDto.ReportedById);
        if (reportedBy == null)
            throw new KeyNotFoundException("Reported by not found");
        var reportedUser = await _bookingUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == createComplaintDto.ReportedById);
        if (reportedUser == null)
            throw new KeyNotFoundException("Reported user not found");
        
        await _bookingUnitOfWork.ComplaintRepository.AddAsync(complaint);
            
        await _bookingUnitOfWork.SaveChangesAsync();
            
    }
    
    public async Task UpdateComplaint(Guid id, UpdateComplaintDto updateComplaintDto)
    {
        var complaint = await _bookingUnitOfWork.ComplaintRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsIgnoreAutoIncludes = true
        });
        if(complaint == null)
            throw new KeyNotFoundException("Complaint not found");
        _bookingUnitOfWork.ComplaintRepository.Detach(complaint);
        
        var updateComplaint = _mapper.Map<PartialComplaints>(updateComplaintDto);
        _bookingUnitOfWork.ComplaintRepository.Update(updateComplaint, complaint);
        
        await _bookingUnitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<ComplaintResponseDto>> GetComplaintByCustomerId(string id, int? page, int? limit)
    {
        var complaints = _bookingUnitOfWork.ComplaintRepository.Find(
            entity => id == entity.ReportedById,
            x => x.CreatedAt,
            true,
            page,
            limit,
            new FindOptions()
            {
                IsIgnoreAutoIncludes = true
            });
        var totalCount = complaints.Count();
        var complaintDtos = _mapper.Map<ComplaintResponseDto[]>(complaints);
        
        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalCount;
        
        return new Pagination<ComplaintResponseDto>(complaintDtos,totalCount, currentPage, currentLimit);
    }
    
}