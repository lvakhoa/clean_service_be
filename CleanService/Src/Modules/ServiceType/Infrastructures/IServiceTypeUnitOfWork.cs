using CleanService.Src.Repositories.ServiceCategory;
using CleanService.Src.Repositories.ServiceType;

namespace CleanService.Src.Modules.ServiceType.Infrastructures;

public interface IServiceTypeUnitOfWork
{
    IServiceTypeRepository ServiceTypeRepository { get; }
    
    IServiceCategoryRepository ServiceCategoryRepository { get; }
     
    Task SaveChangesAsync();
}