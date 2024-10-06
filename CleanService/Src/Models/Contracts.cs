using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(BookingId), IsUnique = true)]
public class Contracts
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Bookings))]
    [Required]
    [MaxLength(255)]
    public Guid BookingId { get; set; }
    
    public string? Content { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public virtual Bookings Booking { get; set; } = null!;
}