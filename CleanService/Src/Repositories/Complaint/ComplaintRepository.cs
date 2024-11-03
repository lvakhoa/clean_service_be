using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Complaint;

public class ComplaintRepository : Repository<Complaints>, IComplaintRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public ComplaintRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}