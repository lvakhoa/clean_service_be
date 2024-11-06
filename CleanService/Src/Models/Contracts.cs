using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

public class Contracts
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Booking))]
    [Required]
    public Guid BookingId { get; set; }
    
    public string? Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public virtual Bookings Booking { get; set; } = null!;
}

public class PartialContracts
{
    //public Guid? Id { get; set; }
    
    public Guid? BookingId { get; set; }
    
    public string? Content { get; set; }
    
    public DateTime? CreatedAt { get; set; }
}