using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(HelperId), nameof(StartDatetime), nameof(EndDatetime))]
[Index(nameof(Status))]
public class HelperAvailability
{
    [Key] public Guid Id { get; set; }

    [ForeignKey(nameof(Helper))]
    [Required]
    public string HelperId { get; set; } = null!;

    [Required] public DateTime StartDatetime { get; set; }

    [Required] public DateTime EndDatetime { get; set; }

    [Required]
    [Column(TypeName = "varchar(24)")]
    public AvailabilityType Type { get; set; }

    [Required]
    [Column(TypeName = "varchar(24)")]
    public AvailabilityStatus Status { get; set; }

    public string? RequestReason { get; set; }

    public string? RejectionReason { get; set; }

    [ForeignKey(nameof(UserApproved))] public string? ApprovedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Helpers Helper { get; set; } = null!;

    public virtual Users UserApproved { get; set; } = null!;
}

public enum AvailabilityType
{
    Vacation,
    SickLeave,
    PersonalLeave,
    Unavailable
}

public enum AvailabilityStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled
}

public class PartialHelperAvailability
{
    public string? HelperId { get; set; } 

    public DateTime? StartDatetime { get; set; }

    public DateTime? EndDatetime { get; set; }

    public AvailabilityType? Type { get; set; }
    
    public AvailabilityStatus? Status { get; set; }

    public string? RequestReason { get; set; }
    
    public string? RejectionReason { get; set; }
    
    public string? ApprovedBy { get; set; }
}