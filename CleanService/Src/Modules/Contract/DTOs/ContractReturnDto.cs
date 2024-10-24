namespace CleanService.Src.Modules.Contract.DTOs;

public class ContractReturnDto
{
    public Guid Id { get; set; }
    
    public Guid BookingId { get; set; }
    
    public string? Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
}