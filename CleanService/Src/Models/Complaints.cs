using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CleanService.Src.Models;

public class Complaints
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Booking))]
    [Required]
    public Guid BookingId { get; set; }
    
    [ForeignKey(nameof(ReportedBy))]
    [Required]
    public string ReportedById { get; set; } = null!;
    
    [ForeignKey(nameof(ReportedUser))]
    [Required]
    public string ReportedUserId { get; set; } = null!;
    
    [Required]
    public string Reason { get; set; } = null!;
    
    [Required]
    public ComplaintStatus Status { get; set; }
    
    public string? Resolution { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ResolvedAt { get; set; }
    
    public virtual Bookings Booking { get; set; } = null!;
    
    public virtual Users ReportedBy { get; set; } = null!;
    
    public virtual Users ReportedUser { get; set; } = null!;
}

public enum ComplaintStatus 
{
    Pending,
    Investigating,
    Resolved,
    Dismissed
}

public class PartialComplaints
{
    public Guid? Id { get; set; }
    
    public Guid? BookingId { get; set; }
    
    public string? ReportedById { get; set; } = null!;
    
    public string? ReportedUserId { get; set; } = null!;
    
    public string? Reason { get; set; } = null!;
    
    public ComplaintStatus? Status { get; set; }
    
    public string? Resolution { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? ResolvedAt { get; set; }
}