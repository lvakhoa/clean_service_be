using EntityFramework.Exceptions.MySQL.Pomelo;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

public class CleanServiceContext : DbContext
{
    public CleanServiceContext(DbContextOptions<CleanServiceContext> options) : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; } = null!;

    public virtual DbSet<Helpers> Helpers { get; set; } = null!;

    public virtual DbSet<Services> Services { get; set; } = null!;

    public virtual DbSet<Bookings> Bookings { get; set; } = null!;

    public virtual DbSet<Contracts> Contracts { get; set; } = null!;

    public virtual DbSet<RestrictedList> RestrictedList { get; set; } = null!;
    
    public virtual DbSet<Notifications> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users entity config
        modelBuilder.Entity<Users>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Users>()
            .Property(u => u.Status)
            .HasDefaultValue(UserStatus.Active);

        // Helpers entity config
        modelBuilder.Entity<Helpers>()
            .Property(h => h.AverageRating)
            .HasDefaultValue(0);
        modelBuilder.Entity<Helpers>()
            .Property(h => h.CompletedJobs)
            .HasDefaultValue(0);
        modelBuilder.Entity<Helpers>()
            .Property(h => h.CancelledJobs)
            .HasDefaultValue(0);
        
        modelBuilder.Entity<Helpers>()
            .HasOne(h => h.User)
            .WithOne(u => u.Helper)
            .HasForeignKey<Helpers>(h => h.Id);
        
        // Bookings entity config
        modelBuilder.Entity<Bookings>()
            .Property(b => b.Status)
            .HasDefaultValue(BookingStatus.Pending);
        
        // Contracts entity config
        modelBuilder.Entity<Contracts>()
            .Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.Now);

        // Notifications entity config
        modelBuilder.Entity<Notifications>()
            .Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Notifications>()
            .Property(c => c.IsRead)
            .HasDefaultValue(false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}