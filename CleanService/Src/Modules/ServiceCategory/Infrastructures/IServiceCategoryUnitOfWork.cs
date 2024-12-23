using CleanService.Src.Repositories.ServiceCategory;

namespace CleanService.Src.Modules.ServiceCategory.Infrastructures;

public interface IServiceCategoryUnitOfWork
{
    IServiceCategoryRepository ServiceCategoryRepository { get; }
    
    Task SaveChangesAsync();
}