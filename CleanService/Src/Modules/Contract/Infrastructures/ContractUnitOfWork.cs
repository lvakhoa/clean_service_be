using CleanService.Src.Models;
using CleanService.Src.Repositories.Contract;

namespace CleanService.Src.Modules.Contract.Infrastructures;

public class ContractUnitOfWork : IContractUnitOfWork
{
    private readonly CleanServiceContext _dbContext;

    public IContractRepository ContractRepository { get; }

    public ContractUnitOfWork(CleanServiceContext dbContext,
        IContractRepository contractRepository)
    {
        _dbContext = dbContext;
        ContractRepository = contractRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}