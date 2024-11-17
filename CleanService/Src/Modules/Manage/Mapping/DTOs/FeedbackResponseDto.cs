using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class FeedbackResponseDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    [Precision(2, 1)]
    public decimal? HelperRating { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? CustomerAvatar { get; set; }
    public string? CustomerName { get; set; }
    public DateTime CreatedAt { get; set; }
}