namespace CleanService.Src.Modules.Contract.DTOs;

public class UpdateContractDto
{
    public Guid? BookingId { get; set; }
    
    public string? Content { get; set; }
}