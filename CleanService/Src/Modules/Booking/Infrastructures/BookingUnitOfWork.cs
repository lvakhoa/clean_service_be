using CleanService.Src.Models;
using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.BookingContract;
using CleanService.Src.Repositories.Contract;
using CleanService.Src.Repositories.DurationPrices;
using CleanService.Src.Repositories.Feedback;
using CleanService.Src.Repositories.Refund;
using CleanService.Src.Repositories.RoomPricings;
using CleanService.Src.Repositories.ServiceType;
using CleanService.Src.Repositories.User;

namespace CleanService.Src.Modules.Booking.Infrastructures;

public class BookingUnitOfWork : IBookingUnitOfWork
{
    private readonly CleanServiceContext _dbContext;
    
    public IBookingRepository BookingRepository { get; }
    
    public IUserRepository UserRepository { get; }

    public IServiceTypeRepository ServiceTypeRepository { get; }

    public IDurationPriceRepository DurationPriceRepository { get; }

    public IRoomPricingRepository RoomPricingRepository { get; }

    public IBookingContractRepository BookingContractRepository { get; }
    
    public IRefundRepository RefundRepository { get; }
    
    public IFeedbackRepository FeedbackRepository { get; }

    public BookingUnitOfWork(
        CleanServiceContext dbContext, 
        IBookingRepository bookingRepository, 
        IUserRepository userRepository, 
        IServiceTypeRepository serviceTypeRepository,
        IDurationPriceRepository durationPriceRepository, 
        IRoomPricingRepository roomPricingRepository,
        IBookingContractRepository bookingContractRepository,
        IRefundRepository refundRepository,
        IFeedbackRepository feedbackRepository)
    {
        _dbContext = dbContext;
        BookingRepository = bookingRepository;
        UserRepository = userRepository;
        ServiceTypeRepository = serviceTypeRepository;
        DurationPriceRepository = durationPriceRepository;
        RoomPricingRepository = roomPricingRepository;
        BookingContractRepository = bookingContractRepository;
        RefundRepository = refundRepository;
        FeedbackRepository = feedbackRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    
}