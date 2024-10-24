namespace CleanService.Src.Modules.Contract.DTOs;

public class CreateContractDto
{
    public Guid BookingId { get; set; }
    
    public string? Content { get; set; }
}