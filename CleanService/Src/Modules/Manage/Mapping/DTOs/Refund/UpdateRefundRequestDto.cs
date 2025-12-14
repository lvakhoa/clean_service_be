using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;

public class UpdateRefundRequestDto
{
    public string BookingId { get; set; } = null!;
    [EnumDataType(typeof(RefundStatus), ErrorMessage = "Invalid Refund Status specified.")]
    public RefundStatus? Status { get; set; }
    public string? Reason { get; set; }
    public DateTime? ResolvedAt { get; set; }
}

