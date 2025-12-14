using CleanService.Src.Models;
using CleanService.Src.Models.Domains;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class FeedbackSpecification
{
    public static BaseSpecification<Feedbacks> GetFeedbackByCustomerIdSpec(string? customerId)
    {
        return new BaseSpecification<Feedbacks>(customerId != null ? x => x.Booking.CustomerId == customerId : null);
    }

    public static BaseSpecification<Feedbacks> GetFeedbackByIdSpec(Guid id)
    {
        return new BaseSpecification<Feedbacks>(x => x.Id == id);
    }
}
