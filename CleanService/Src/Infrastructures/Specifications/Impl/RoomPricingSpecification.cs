using CleanService.Src.Models;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class RoomPricingSpecification
{
    public static BaseSpecification<RoomPricing> GetRoomPricingByTypeAndCountSpec(RoomType type, int count)
    {
        return new BaseSpecification<RoomPricing>(x => x.RoomType == type && x.RoomCount == count);
    }

    public static BaseSpecification<RoomPricing> GetRoomPricingByRoomTypeOrServiceTypeSpec(RoomType? roomType,
        Guid? serviceTypeId)
    {
        return new BaseSpecification<RoomPricing>(x =>
            (!roomType.HasValue || x.RoomType == roomType) &&
            (!serviceTypeId.HasValue || x.ServiceTypeId == serviceTypeId));
    }

    public static BaseSpecification<RoomPricing> GetRoomPricingByIdSpec(Guid id)
    {
        return new BaseSpecification<RoomPricing>(x => x.Id == id);
    }
}
