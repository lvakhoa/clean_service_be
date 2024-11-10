namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class RefundResponseDto
{
    public string Id { get; set; } = null!;
    
    public string? HelperId { get; set; } 
    public string? HelperName { get; set; } 
    
    public string CustomerId { get; set; } = null!;
    
    public string CustomerName { get; set; } = null!;
    public string Reason { get; set; } = null!;
    
    public string Status { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ResolvedAt { get; set; }
}