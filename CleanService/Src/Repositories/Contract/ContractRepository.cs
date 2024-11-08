using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Contract;

public class ContractRepository : Repository<Contracts, PartialContracts>, IContractRepository
{
    private readonly CleanServiceContext _dbContext;

    public ContractRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}