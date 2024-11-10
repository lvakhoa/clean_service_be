namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class ComplaintResponseDto
{
    public string Id { get; set; } = null!;
    
    public string ReportedUserId { get; set; } = null!;
    public string ReportedUserName { get; set; } = null!;
    
    public string ReporterId { get; set; } = null!;
    
    public string ReporterUserName { get; set; } = null!;
    public string Reason { get; set; } = null!;
    
    public string Status { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ResolvedAt { get; set; }
}