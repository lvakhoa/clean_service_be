using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(Email), IsUnique = true)]
public class Users
{
    [Key] [MaxLength(255)] public string Id { get; set; } = null!;

    [Column(TypeName = "varchar(24)")]
    [Required]
    public UserType UserType { get; set; }

    [Column(TypeName = "varchar(24)")] public Gender? Gender { get; set; }

    public string? ProfilePicture { get; set; }

    [Required] [MaxLength(150)] public string FullName { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string? IdentityCard { get; set; }

    [MaxLength(255)] public string? Address { get; set; }

    [MaxLength(20)]
    [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [Required]
    [Column(TypeName = "varchar(24)")]
    public UserStatus Status { get; set; }

    public string? NotificationToken { get; set; }

    public virtual Helpers? Helper { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Bookings> CustomerBookings { get; set; } = new List<Bookings>();

    [InverseProperty("Helper")]
    public virtual ICollection<Bookings> HelperBookings { get; set; } = new List<Bookings>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    [InverseProperty("ReportedBy")]
    public virtual ICollection<Complaints> CreatedComplaints { get; set; } = new List<Complaints>();

    [InverseProperty("ReportedUser")]
    public virtual ICollection<Complaints> ReceivedComplaints { get; set; } = new List<Complaints>();

    [InverseProperty("User")]
    public virtual ICollection<BlacklistedUsers> BlacklistedUsers { get; set; } = new List<BlacklistedUsers>();

    [InverseProperty("BlacklistedByUser")]
    public virtual ICollection<BlacklistedUsers> BlacklistedByUsers { get; set; } = new List<BlacklistedUsers>();
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
    Inactive,
    Blocked
}

public enum Gender
{
    Male,
    Female,
    Other
}

public class PartialUsers
{
    public string? Id { get; set; }

    public UserType? UserType { get; set; }

    public Gender? Gender { get; set; }

    public string? ProfilePicture { get; set; }

    public string? FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? IdentityCard { get; set; }

    public string? Address { get; set; }

    [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; } = null!;

    [EmailAddress] public string? Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public UserStatus? Status { get; set; }

    public string? NotificationToken { get; set; }
}