namespace CleanService.Src.Modules.Contract.Mapping.DTOs;

public class CreateContractDto
{
    public Guid BookingId { get; set; }
    
    public string? Content { get; set; }
}