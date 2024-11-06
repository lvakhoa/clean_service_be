using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(UserId), IsUnique = true)]
public class BlacklistedUsers
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(User))]
    [Required]
    public string UserId { get; set; } = null!;
    
    [Required]
    public string Reason { get; set; } = null!;
    
    [Required]
    public DateTime BlacklistedAt { get; set; }
    
    [ForeignKey(nameof(BlacklistedByUser))]
    [Required]
    public string BlacklistedBy { get; set; } = null!;
    
    [Required]
    public bool IsPermanent { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
    
    public virtual Users User { get; set; } = null!;
    
    public virtual Users BlacklistedByUser { get; set; } = null!;
}

public class PartialBlacklistedUsers
{
    //public Guid? Id { get; set; }
    
    public string? UserId { get; set; } = null!;
    
    public string? Reason { get; set; } = null!;
    
    public DateTime? BlacklistedAt { get; set; }
    
    public string? BlacklistedBy { get; set; } = null!;
    
    public bool? IsPermanent { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}