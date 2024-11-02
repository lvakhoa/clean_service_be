using CleanService.Src.Models;

namespace CleanService.Src.Modules.ServicePricing.Repositories;

public interface IServicePricingRepository
{
    public Task<RoomPricing[]> GetAllRoomPricingType();

    public Task<RoomPricing?> GetRoomPricingTypeById(Guid id);

    public Task<RoomPricing?> GetUniqueRoomPricingType(RoomType roomType, int roomCount);

    public Task<RoomPricing> CreateRoomPricingType(RoomPricing roomPricing);

    public Task<RoomPricing?> UpdateRoomPricingType(Guid id, PartialRoomPricing roomPricing);

    public Task<RoomPricing?> DeleteRoomPricingType(Guid id);

    public Task<DurationPrice[]> GetAllDurationPriceType();

    public Task<DurationPrice?> GetDurationPriceTypeById(Guid id);
    
    public Task<DurationPrice> CreateDurationPriceType(DurationPrice durationPrice);
    
    public Task<DurationPrice?> UpdateDurationPriceType(Guid id, PartialDurationPrice durationPrice);
    
    public Task<DurationPrice?> DeleteDurationPriceType(Guid id);
}