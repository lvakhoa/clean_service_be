using CleanService.Src.Models;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class ContractSpecification
{
    public static BaseSpecification<Contracts> GetContractByIdSpec(Guid id)
    {
        return new BaseSpecification<Contracts>(x => x.Id == id);
    }

    public static BaseSpecification<Contracts> GetPagedContractsSpec(int? page, int? limit)
    {
        var spec = new BaseSpecification<Contracts>(null);
        if (page.HasValue && limit.HasValue)
        {
            spec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }
        return spec;
    }
}
