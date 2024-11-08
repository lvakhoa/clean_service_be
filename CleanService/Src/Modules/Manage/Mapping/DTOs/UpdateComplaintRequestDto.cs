using CleanService.Src.Models;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class UpdateComplaintRequestDto
{
    public ComplaintStatus? Status { get; set; }

    public string? Reason { get; set; }
    
    public string? Resolution { get; set; }
}

