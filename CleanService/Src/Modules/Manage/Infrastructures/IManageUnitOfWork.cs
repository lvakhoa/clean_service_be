
using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.Contract;
using CleanService.Src.Repositories.DurationPrices;
using CleanService.Src.Repositories.RoomPricings;
using CleanService.Src.Repositories.ServiceType;
using CleanService.Src.Repositories.User;

namespace CleanService.Src.Modules.Manage.Infrastructures;

public interface IManageUnitOfWork
{
    IBookingRepository BookingRepository { get; }
    
    IUserRepository UserRepository { get; }
    
    IServiceTypeRepository ServiceTypeRepository { get; }
    
    IDurationPriceRepository DurationPriceRepository { get; }
    
    IRoomPricingRepository RoomPricingRepository { get; }
    
    IContractRepository ContractRepository { get; }
    
    Task SaveChangesAsync();
}