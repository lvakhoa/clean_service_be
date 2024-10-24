namespace CleanService.Src.Modules.Service.DTOs;

public class ServiceReturnDto
{
    public Guid Id { get; set; }
    
    public string ServiceName { get; set; } = null!;
    
    public string? Description { get; set; }
}