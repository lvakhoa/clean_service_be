using CleanService.Src.Models;
using CleanService.Src.Models.Domains;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class ServiceTypeSpecification
{
    public static BaseSpecification<ServiceTypes> GetServiceTypeByIdSpec(Guid id)
    {
        return new BaseSpecification<ServiceTypes>(x => x.Id == id);
    }

    public static BaseSpecification<ServiceTypes> GetServiceTypeByCategoryIdSpec(Guid? categoryId)
    {
        return new BaseSpecification<ServiceTypes>(categoryId != null ? x => x.CategoryId == categoryId : null);
    }
}
