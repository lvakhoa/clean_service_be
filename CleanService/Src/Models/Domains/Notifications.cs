using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CleanService.Src.Common;
using CleanService.Src.Models.Configurations;

namespace CleanService.Src.Models;

public class Notifications : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
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

public class PartialNotification
{
    //public Guid? Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public NotificationType? Type { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? ReferenceId { get; set; }

    public bool? IsRead { get; set; }
}
