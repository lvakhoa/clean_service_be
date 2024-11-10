using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Refund;

public class RefundRepository : Repository<Refunds, PartialRefunds>, IRefundRepository
{
    private readonly CleanServiceContext _dbContext;

    public RefundRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}