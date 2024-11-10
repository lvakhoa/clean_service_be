using CleanService.Src.Models;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class UpdateRefundRequestDto
{
    public string BookingId { get; set; } = null!;
    public RefundStatus? Status { get; set; }

    public string? Reason { get; set; }
    
}

