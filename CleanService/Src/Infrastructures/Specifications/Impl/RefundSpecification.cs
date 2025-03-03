using CleanService.Src.Models;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class RefundSpecification
{
    public static BaseSpecification<Refunds> GetRefundByIdSpec(Guid id)
    {
        return new BaseSpecification<Refunds>(x => x.Id == id);
    }

    public static BaseSpecification<Refunds> GetRefundByCustomerIdSpec(string? customerId)
    {
        return new BaseSpecification<Refunds>(customerId != null ? x => x.Booking.CustomerId == customerId : null);
    }

    public static BaseSpecification<Refunds> GetRefundByStatusSpec(RefundStatus? status)
    {
        return new BaseSpecification<Refunds>(status != null ? x => x.Status == status : null);
    }

    public static BaseSpecification<Refunds> GetPagedRefundsSpec(int? page, int? limit)
    {
        var spec = new BaseSpecification<Refunds>(null);
        if (page.HasValue && limit.HasValue)
        {
            spec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        return spec;
    }
}
