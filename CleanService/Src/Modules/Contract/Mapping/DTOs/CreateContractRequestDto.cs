namespace CleanService.Src.Modules.Contract.Mapping.DTOs;

public class CreateContractRequestDto
{
    public Guid BookingId { get; set; }
    
    public string? Content { get; set; }
}