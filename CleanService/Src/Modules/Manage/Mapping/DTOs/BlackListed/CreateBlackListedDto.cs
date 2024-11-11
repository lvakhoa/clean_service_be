namespace CleanService.Src.Modules.Manage.Mapping.DTOs.BlackListed;

public class CreateBlackListedDto
{
    public string UserId { get; set; } = null!;
    
    public string Reason { get; set; } = null!;
    
    public string BlacklistedBy { get; set; } = null!;
    
    public bool IsPermanent { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}