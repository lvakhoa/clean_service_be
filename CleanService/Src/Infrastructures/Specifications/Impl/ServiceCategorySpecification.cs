using CleanService.Src.Models;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class ServiceCategorySpecification
{
    public static BaseSpecification<ServiceCategories> GetServiceCategoryById(Guid id)
    {
        return new BaseSpecification<ServiceCategories>(x => x.Id == id);
    }

    public static BaseSpecification<ServiceCategories> GetPagedServiceCategory(int? page, int? limit)
    {
        var spec = new BaseSpecification<ServiceCategories>(null);
        if (page.HasValue && limit.HasValue)
        {
            spec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }
        return spec;
    }
}
