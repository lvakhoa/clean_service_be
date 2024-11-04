using CleanService.Src.Repositories.Contract;

namespace CleanService.Src.Modules.Contract.Infrastructures;

public interface IContractUnitOfWork
{
    IContractRepository ContractRepository { get; }
    
    Task SaveChangesAsync();
}