using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanService.Src.Models;

public class Notifications
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Users))]
    [Required]
    public string UserId { get; set; } = null!;
    
    [Required]
    public string Title { get; set; } = null!;
    
    [Required]
    public string Content { get; set; } = null!;
    
    [Required]
    [Column(TypeName = "varchar(24)")]
    public NotificationType Type { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid? ReferenceId { get; set; }
    
    public bool IsRead { get; set; }
    
    public virtual Users User { get; set; } = null!;
}

public enum NotificationType
{
    Booking,
    Scheduling,
    Complaint
}