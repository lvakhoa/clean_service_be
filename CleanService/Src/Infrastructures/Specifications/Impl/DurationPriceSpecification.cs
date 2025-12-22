using CleanService.Src.Models;
using CleanService.Src.Models.Domains;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class DurationPriceSpecification
{
    public static BaseSpecification<DurationPrice> GetDurationPriceByIdSpec(Guid id)
    {
        return new BaseSpecification<DurationPrice>(x => x.Id == id);
    }

    public static BaseSpecification<DurationPrice> GetDurationPriceByIdAndServiceTypeSpec(Guid id, Guid serviceTypeId)
    {
        return new BaseSpecification<DurationPrice>(x => x.Id == id && x.ServiceTypeId == serviceTypeId);
    }

    public static BaseSpecification<DurationPrice> GetDurationPriceOrderByDurationHoursSpec()
    {
        var spec = new BaseSpecification<DurationPrice>(null);
        spec.ApplyOrderBy(x => x.DurationHours);
        return spec;
    }
}
