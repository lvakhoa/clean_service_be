using System.Net;

using AutoMapper;

using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Payment.Services;
using CleanService.Src.Modules.Mail.Services;
using CleanService.Src.Utils;

using Microsoft.EntityFrameworkCore;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public BookingService(IUnitOfWork unitOfWork, IPaymentService paymentService, IMapper mapper, IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _paymentService = paymentService;
        _mapper = mapper;
        _mailService = mailService;
    }

    public async Task<string> CreateBooking(CreateBookingRequestDto createBookingDto)
    {
        var booking = _mapper.Map<Bookings>(createBookingDto);

        var customerSpec = UserSpecification.GetUserByIdSpec(createBookingDto.CustomerId);
        var customer = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstOrThrowAsync(customerSpec);

        var serviceTypeSpec = ServiceTypeSpecification.GetServiceTypeByIdSpec(booking.ServiceTypeId);
        var serviceType = await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>()
            .GetFirstOrThrowAsync(serviceTypeSpec);

        booking.HelperId = await AssignHelperToBooking(booking, customer);
        if (booking.HelperId == null)
        {
            booking.Status = BookingStatus.Cancelled;
            throw new UnprocessableRequestException("No helper available for the requested time",
                exceptionCode: ExceptionConvention.NoHelperAvailable);
        }
        else
        {
            booking.Status = BookingStatus.Confirmed;
        }

        /*
         Calculate the pricing for booking based on:
         - Room Type (Bedroom, Bathroom, Kitchen, Living Room)
         - Duration Price
         */
        var basePrice = serviceType.BasePrice;
        var bedroomPricing = (await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>()
            .GetFirstAsync(RoomPricingSpecification.GetRoomPricingByTypeAndCountAndServiceTypeSpec(RoomType.Bedroom,
                booking.BookingDetails.BedroomCount, booking.ServiceTypeId)))?.AdditionalPrice ?? 0;
        var bathroomPricing = (await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>()
            .GetFirstAsync(RoomPricingSpecification.GetRoomPricingByTypeAndCountAndServiceTypeSpec(RoomType.Bathroom,
                booking.BookingDetails.BathroomCount, booking.ServiceTypeId)))?.AdditionalPrice ?? 0;
        var kitchenPricing = (await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>()
            .GetFirstAsync(RoomPricingSpecification.GetRoomPricingByTypeAndCountAndServiceTypeSpec(RoomType.Kitchen,
                booking.BookingDetails.KitchenCount, booking.ServiceTypeId)))?.AdditionalPrice ?? 0;
        var livingRoomPricing = (await _unitOfWork.Repository<RoomPricing, PartialRoomPricing>()
            .GetFirstAsync(RoomPricingSpecification.GetRoomPricingByTypeAndCountAndServiceTypeSpec(RoomType.LivingRoom,
                booking.BookingDetails.LivingRoomCount, booking.ServiceTypeId)))?.AdditionalPrice ?? 0;
        var durationPricing = (await _unitOfWork.Repository<DurationPrice, PartialDurationPrice>()
            .GetFirstAsync(DurationPriceSpecification.GetDurationPriceByIdAndServiceTypeSpec(
                booking.BookingDetails.DurationPriceId ?? Guid.Empty, booking.ServiceTypeId)))?.PriceMultiplier ?? 0;

        var totalPrice = basePrice + bedroomPricing + bathroomPricing + kitchenPricing + livingRoomPricing +
                         basePrice * durationPricing;
        booking.TotalPrice = totalPrice;

        while (true)
        {
            var bookingOrderId = new Random().Next(100000, 999999);

            var existingOrderId = await _unitOfWork.Repository<Bookings, PartialBookings>()
                .AnyAsync(BookingSpecification.GetBookingByOrderIdSpec(bookingOrderId));
            if (existingOrderId) continue;

            booking.OrderId = bookingOrderId;
            break;
        }

        await _unitOfWork.Repository<Bookings, PartialBookings>().AddAsync(booking);

        await _unitOfWork.Repository<BookingContracts, PartialBookingContracts>().AddAsync(new BookingContracts()
        {
            BookingId = booking.Id, Content = createBookingDto.ContractContent
        });

        var paymentLink = await _paymentService.CreatePaymentLink(booking);

        await _unitOfWork.SaveChangesAsync();

        // Send email to customer
        try
        {
            var customerEmailBody = $@"
                <html>
                <body>
                    <h2>Booking Confirmation</h2>
                    <p>Dear {customer.FullName},</p>
                    <p>Your booking has been successfully created!</p>
                    <h3>Booking Details:</h3>
                    <ul>
                        <li><strong>Order ID:</strong> {booking.OrderId}</li>
                        <li><strong>Service:</strong> {serviceType.Name}</li>
                        <li><strong>Scheduled Start:</strong> {booking.ScheduledStartTime:yyyy-MM-dd HH:mm}</li>
                        <li><strong>Scheduled End:</strong> {booking.ScheduledEndTime:yyyy-MM-dd HH:mm}</li>
                        <li><strong>Total Price:</strong> ${booking.TotalPrice:F2}</li>
                        <li><strong>Status:</strong> {booking.Status}</li>
                    </ul>
                    <p><strong style=""color: #d9534f;"">⚠️ IMPORTANT: Please complete your payment within 15 minutes to confirm your booking.</strong></p>
                    <p>Click the button below to proceed with payment:</p>
                    <p><a href=""{paymentLink}"" style=""display: inline-block; padding: 10px 20px; background-color: #5cb85c; color: white; text-decoration: none; border-radius: 5px;"">Pay Now</a></p>
                    <p>Thank you for choosing our cleaning service!</p>
                </body>
                </html>
            ";

            await _mailService.SendMail(customer.Email, "Booking Confirmation - Order #" + booking.OrderId, customerEmailBody);
        }
        catch (Exception ex)
        {
            // Log the error but don't fail the booking creation
            Console.WriteLine($"Failed to send email to customer: {ex.Message}");
        }

        // Send email to helper
        if (booking.HelperId != null)
        {
            try
            {
                var helperSpec = UserSpecification.GetUserByIdSpec(booking.HelperId);
                var helper = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstAsync(helperSpec);

                if (helper != null)
                {
                    var helperEmailBody = $@"
                        <html>
                        <body>
                            <h2>New Booking Assignment</h2>
                            <p>Dear {helper.FullName},</p>
                            <p>You have been assigned to a new booking!</p>
                            <h3>Booking Details:</h3>
                            <ul>
                                <li><strong>Order ID:</strong> {booking.OrderId}</li>
                                <li><strong>Service:</strong> {serviceType.Name}</li>
                                <li><strong>Customer:</strong> {customer.FullName}</li>
                                <li><strong>Customer Phone:</strong> {customer.PhoneNumber ?? "N/A"}</li>
                                <li><strong>Address:</strong> {booking.Location}</li>
                                <li><strong>Scheduled Start:</strong> {booking.ScheduledStartTime:yyyy-MM-dd HH:mm}</li>
                                <li><strong>Scheduled End:</strong> {booking.ScheduledEndTime:yyyy-MM-dd HH:mm}</li>
                                <li><strong>Status:</strong> {booking.Status}</li>
                            </ul>
                            <p>Please be ready for this assignment at the scheduled time.</p>
                            <p>Thank you!</p>
                        </body>
                        </html>
                    ";

                    await _mailService.SendMail(helper.Email, "New Booking Assignment - Order #" + booking.OrderId, helperEmailBody);
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the booking creation
                Console.WriteLine($"Failed to send email to helper: {ex.Message}");
            }
        }

        return paymentLink;
    }

    public async Task UpdateBooking(Guid id, UpdateBookingRequestDto updateBookingDto)
    {
        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>()
            .GetFirstOrThrowAsync(BookingSpecification.GetBookingByIdSpec(id));
        await _unitOfWork.Repository<Bookings, PartialBookings>().Detach(booking);

        var updateBooking = _mapper.Map<PartialBookings>(updateBookingDto);
        await _unitOfWork.Repository<Bookings, PartialBookings>().UpdateAsync(updateBooking, booking);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<BookingResponseDto>> GetAllBookings(int? page, int? limit)
    {
        var bookingSpec = BookingSpecification.GetPagedBookingsSpec(page, limit);
        var bookings = _unitOfWork.Repository<Bookings, PartialBookings>().GetAllAsync(bookingSpec);
        var totalBookings = await _unitOfWork.Repository<Bookings, PartialBookings>().CountAsync();

        var bookingDtos = _mapper.Map<BookingResponseDto[]>(bookings);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalBookings;

        return new Pagination<BookingResponseDto>(bookingDtos, totalBookings, currentPage, currentLimit);
    }

    public async Task<Pagination<CusRefundResponseDto>> GetAllComplaints(int? page, int? limit)
    {
        var refundSpec = RefundSpecification.GetPagedRefundsSpec(page, limit);
        var complaints = _unitOfWork.Repository<Refunds, PartialRefunds>().GetAllAsync(refundSpec);
        var totalComplaints = await _unitOfWork.Repository<Refunds, PartialRefunds>().CountAsync();

        var complaintDtos = _mapper.Map<CusRefundResponseDto[]>(complaints);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalComplaints;

        return new Pagination<CusRefundResponseDto>(complaintDtos, totalComplaints, currentPage, currentLimit);
    }

    public async Task<BookingResponseDto?> GetBookingById(Guid id)
    {
        return await _unitOfWork.Repository<Bookings, PartialBookings>()
            .GetFirstOrThrowAsync(BookingSpecification.GetBookingByIdSpec(id))
            .ContinueWith(task => _mapper.Map<BookingResponseDto>(task.Result));
    }

    public async Task<string?> AssignHelperToBooking(Bookings booking, Users customer)
    {
        // Get the list of all helpers
        var allHelpers = await _unitOfWork.Repository<Helpers, PartialHelper>().GetAllAsync();
        var availableHelpers = new List<Helpers>();

        int? minJobTaken = null;
        var relevantStatuses = new List<BookingStatus> { BookingStatus.Confirmed, BookingStatus.InProgress };

        foreach (var helper in allHelpers)
        {
            // Get the list of bookings for the current helper
            var helperBookings = await _unitOfWork.Repository<Bookings, PartialBookings>()
                .GetAllAsync(
                    BookingSpecification.GetBookingByUserIdAndStatusSpec(helper.Id, UserType.Helper, relevantStatuses));
            var jobCount = helperBookings.Count(x =>
                x.Status is BookingStatus.Completed or BookingStatus.Confirmed or BookingStatus.InProgress);

            // Check if the helper is available during the requested time
            var isAvailable = helperBookings.All(x =>
                !relevantStatuses.Contains(x.Status) || x.ScheduledEndTime <= booking.ScheduledStartTime ||
                x.ScheduledStartTime >= booking.ScheduledEndTime);

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
        availableHelpers = availableHelpers.Where(x => !customer.BlacklistedByUsers.Any(y => y.UserId == x.Id))
            .ToList();

        // Filter out helpers who do not offer the required service
        // availableHelpers = availableHelpers
        //     .Where(x => x.ServicesOffered != null && x.ServicesOffered.Contains(booking.ServiceTypeId))
        //     .ToList();

        if (!availableHelpers.Any()) return null;

        // Find the most suitable helpers with the least number of jobs taken

        // Randomly choose a helper from the most suitable helpers
        var random = new Random();
        var selectedHelper = availableHelpers[random.Next(availableHelpers.Count)];

        return selectedHelper.Id;
    }


    //Refund service
    public async Task CreateRefund(CreateRefundRequestDto createRefundRequestDto)
    {
        var complaint = _mapper.Map<Refunds>(createRefundRequestDto);

        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>()
            .GetFirstOrThrowAsync(BookingSpecification.GetBookingByIdSpec(complaint.BookingId));

        await _unitOfWork.Repository<Refunds, PartialRefunds>().AddAsync(complaint);

        await _unitOfWork.Repository<Bookings, PartialBookings>().Detach(booking);

        var updateBooking = new PartialBookings() { Status = BookingStatus.Pending, };
        await _unitOfWork.Repository<Bookings, PartialBookings>().UpdateAsync(updateBooking, booking);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRefund(Guid id, CusUpdateRefundRequestDto cusUpdateRefundRequestDto)
    {
        var refund = await _unitOfWork.Repository<Refunds, PartialRefunds>()
            .GetFirstOrThrowAsync(RefundSpecification.GetRefundByIdSpec(id));
        if (refund.BookingId != cusUpdateRefundRequestDto.BookingId)
            throw new KeyNotFoundException("Booking ID does not exist in database");

        await _unitOfWork.Repository<Refunds, PartialRefunds>().Detach(refund);

        var updateRefund = _mapper.Map<PartialRefunds>(cusUpdateRefundRequestDto);
        await _unitOfWork.Repository<Refunds, PartialRefunds>().UpdateAsync(updateRefund, refund);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<CusRefundResponseDto>> GetComplaintByCustomerId(string? id, int? page, int? limit)
    {
        var refundSpec = RefundSpecification.GetRefundByCustomerIdSpec(id);
        refundSpec.ApplyOrderByDescending(x => x.CreatedAt);
        if (page.HasValue && limit.HasValue)
        {
            refundSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var complaints = await _unitOfWork.Repository<Refunds, PartialRefunds>().GetAllAsync(refundSpec);
        var totalCount = complaints.Count();
        var complaintDtos = _mapper.Map<CusRefundResponseDto[]>(complaints);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalCount;

        return new Pagination<CusRefundResponseDto>(complaintDtos, totalCount, currentPage, currentLimit);
    }


    //Feeback service
    public async Task CreateFeedback(CreateFeedbackDto createFeedbackDto)
    {
        // if (createFeedbackDto == null)
        // {
        //     throw new ArgumentNullException(nameof(createFeedbackDto), "Feedback data cannot be null.");
        // }
        // var booking = await _unitOfWork.Repository<Bookings, PartialBookings>().GetFirstOrThrowAsync(
        //     BookingSpecification.GetBookingByIdSpec(createFeedbackDto.BookingId));
        // if (booking.Status != BookingStatus.Completed)
        //     throw new BadRequestException("Cannot create feedback if booking status is not completed.",
        //         ExceptionConvention.BookingStatusNotCompleted);
        //
        // var feedback = _mapper.Map<Feedbacks>(createFeedbackDto);
        // await _unitOfWork.Repository<Feedbacks, PartialFeedback>().AddAsync(feedback);
        //
        // //update booking rating
        // await _unitOfWork.Repository<Bookings, PartialBookings>().Detach(booking);
        //
        // var updateBooking = new PartialBookings() { HelperRating = createFeedbackDto.Rating, };
        // await _unitOfWork.Repository<Bookings, PartialBookings>().UpdateAsync(updateBooking, booking);
        //
        // await _unitOfWork.SaveChangesAsync();

        var booking = await _unitOfWork
            .Repository<Bookings, PartialBookings>()
            .GetFirstOrThrowAsync(
                BookingSpecification.GetBookingByIdSpec(createFeedbackDto.BookingId)
            );

        if (booking.Status != BookingStatus.Completed)
            throw new BadRequestException(
                "Cannot create feedback if booking status is not completed.",
                ExceptionConvention.BookingStatusNotCompleted
            );

        var feedback = _mapper.Map<Feedbacks>(createFeedbackDto);

        await _unitOfWork
            .Repository<Feedbacks, PartialFeedback>()
            .AddAsync(feedback);

        booking.HelperRating = createFeedbackDto.Rating;
        booking.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<CusFeedbackResponseDto>> GetFeedbackByCustomerId(string? id, int? page, int? limit)
    {
        var feedbackSpec = FeedbackSpecification.GetFeedbackByCustomerIdSpec(id);
        feedbackSpec.ApplyOrderByDescending(x => x.CreatedAt);
        if (page.HasValue && limit.HasValue)
        {
            feedbackSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var feedbacks = await _unitOfWork.Repository<Feedbacks, PartialFeedback>().GetAllAsync(feedbackSpec);
        var totalCount = feedbacks.Count;
        var feedbackDtos = _mapper.Map<CusFeedbackResponseDto[]>(feedbacks);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalCount;

        return new Pagination<CusFeedbackResponseDto>(feedbackDtos, totalCount, currentPage, currentLimit);
    }
}
