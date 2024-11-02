using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.ServicePricing.Repositories;

public class ServicePricingRepository : IServicePricingRepository
{
    private readonly CleanServiceContext _context;

    public ServicePricingRepository(CleanServiceContext context)
    {
        _context = context;
    }

    public async Task<RoomPricing[]> GetAllRoomPricingType()
    {
        return await _context.RoomPricing.ToArrayAsync();
    }

    public async Task<RoomPricing?> GetRoomPricingTypeById(Guid id)
    {
        return await _context.RoomPricing.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RoomPricing?> GetUniqueRoomPricingType(RoomType roomType, int roomCount)
    {
        return await _context.RoomPricing.FirstOrDefaultAsync(x => x.RoomType == roomType && x.RoomCount == roomCount);
    }

    public async Task<RoomPricing> CreateRoomPricingType(RoomPricing roomPricing)
    {
        var roomPricingEntity = await _context.RoomPricing.AddAsync(roomPricing);
        await _context.SaveChangesAsync();
        return roomPricingEntity.Entity;
    }

    public async Task<RoomPricing?> UpdateRoomPricingType(Guid id, PartialRoomPricing roomPricing)
    {
        var roomPricingEntity = await _context.RoomPricing.FirstOrDefaultAsync(x => x.Id == id);
        if (roomPricingEntity == null)
            return null;

        if (roomPricing.ServiceTypeId != null)
            roomPricingEntity.ServiceTypeId = roomPricing.ServiceTypeId.Value;
        if (roomPricing.RoomType != null)
            roomPricingEntity.RoomType = roomPricing.RoomType.Value;
        if (roomPricing.RoomCount != null)
            roomPricingEntity.RoomCount = roomPricing.RoomCount.Value;
        if (roomPricing.AdditionalPrice != null)
            roomPricingEntity.AdditionalPrice = roomPricing.AdditionalPrice.Value;
        if (roomPricing.CreatedAt != null)
            roomPricingEntity.CreatedAt = roomPricing.CreatedAt.Value;

        await _context.SaveChangesAsync();
        return roomPricingEntity;
    }

    public async Task<RoomPricing?> DeleteRoomPricingType(Guid id)
    {
        var roomPricingEntity = await _context.RoomPricing.FirstOrDefaultAsync(x => x.Id == id);
        if (roomPricingEntity == null)
            return null;

        _context.RoomPricing.Remove(roomPricingEntity);
        await _context.SaveChangesAsync();
        return roomPricingEntity;
    }

    public async Task<DurationPrice[]> GetAllDurationPriceType()
    {
        return await _context.DurationPrice.ToArrayAsync();
    }

    public async Task<DurationPrice?> GetDurationPriceTypeById(Guid id)
    {
        return await _context.DurationPrice.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<DurationPrice> CreateDurationPriceType(DurationPrice durationPrice)
    {
        var durationPriceEntity = await _context.DurationPrice.AddAsync(durationPrice);
        await _context.SaveChangesAsync();
        return durationPriceEntity.Entity;
    }

    public async Task<DurationPrice?> UpdateDurationPriceType(Guid id, PartialDurationPrice durationPrice)
    {
        var durationPriceEntity = await _context.DurationPrice.FirstOrDefaultAsync(x => x.Id == id);
        if (durationPriceEntity == null)
            return null;

        if (durationPrice.ServiceTypeId != null)
            durationPriceEntity.ServiceTypeId = durationPrice.ServiceTypeId.Value;
        if (durationPrice.DurationHours != null)
            durationPriceEntity.DurationHours = durationPrice.DurationHours.Value;
        if (durationPrice.PriceMultiplier != null)
            durationPriceEntity.PriceMultiplier = durationPrice.PriceMultiplier.Value;
        if (durationPrice.CreatedAt != null)
            durationPriceEntity.CreatedAt = durationPrice.CreatedAt.Value;

        await _context.SaveChangesAsync();
        return durationPriceEntity;
    }

    public async Task<DurationPrice?> DeleteDurationPriceType(Guid id)
    {
        var durationPriceEntity = await _context.DurationPrice.FirstOrDefaultAsync(x => x.Id == id);
        if (durationPriceEntity == null)
            return null;

        _context.DurationPrice.Remove(durationPriceEntity);
        await _context.SaveChangesAsync();
        return durationPriceEntity;
    }
}