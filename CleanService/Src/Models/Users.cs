using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(FullName))]
[Index(nameof(Email), IsUnique = true)]
public class Users
{
    [Key] [MaxLength(255)] public string Id { get; set; } = null!;

    [Column(TypeName = "varchar(24)")]
    [Required] public UserType UserType { get; set; }

    [Required] [MaxLength(255)] public string FullName { get; set; } = null!;

    [MaxLength(255)] public string? Address { get; set; }

    [MaxLength(20)]
    [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\\b/g")]
    public string? PhoneNumber { get; set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "varchar(24)")]
    public UserStatus Status { get; set; }
    
    public string? NotificationToken { get; set; }

    public virtual Helpers? Helper { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Bookings> CustomerBookings { get; set; } = new List<Bookings>();

    [InverseProperty("Helper")]
    public virtual ICollection<Bookings> HelperBookings { get; set; } = new List<Bookings>();

    [InverseProperty("Customer")]
    public virtual ICollection<RestrictedList> CustomerRestrictedList { get; set; } = new List<RestrictedList>();

    [InverseProperty("Helper")]
    public virtual ICollection<RestrictedList> HelperRestrictedList { get; set; } = new List<RestrictedList>();
    
    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();
}

public enum UserType
{
    Customer,
    Helper,
    Admin
}

public enum UserStatus
{
    Active,
    Blocked
}