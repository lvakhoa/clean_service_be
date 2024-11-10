using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Feedback;

public class FeedbackRepository : Repository<Feedbacks, PartialFeedback>, IFeedbackRepository
{
    private readonly CleanServiceContext _dbContext;

    public FeedbackRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}