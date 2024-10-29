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

    public virtual DbSet<ServiceCategories> ServiceCategory { get; set; } = null!;

    public virtual DbSet<ServiceTypes> ServiceTypes { get; set; } = null!;

    public virtual DbSet<RoomPricing> RoomPricing { get; set; } = null!;

    public virtual DbSet<DurationPrice> DurationPrice { get; set; } = null!;

    public virtual DbSet<Bookings> Bookings { get; set; } = null!;

    public virtual DbSet<BookingDetails> BookingDetails { get; set; } = null!;

    public virtual DbSet<Contracts> Contracts { get; set; } = null!;

    public virtual DbSet<Complaints> Complaints { get; set; } = null!;

    public virtual DbSet<BlacklistedUsers> BlacklistedUsers { get; set; } = null!;

    public virtual DbSet<Notifications> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users entity config
        modelBuilder.Entity<Users>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Users>()
            .Property(u => u.UpdatedAt)
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
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Helpers>()
            .Property(u => u.UpdatedAt)
            .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<Helpers>()
            .HasOne(h => h.User)
            .WithOne(u => u.Helper)
            .HasForeignKey<Helpers>(h => h.Id);

        // Service Categories entity config
        modelBuilder.Entity<ServiceCategories>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<ServiceCategories>()
            .Property(u => u.IsActive)
            .HasDefaultValue(true);

        // Service Types entity config
        modelBuilder.Entity<ServiceTypes>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<ServiceTypes>()
            .Property(u => u.IsActive)
            .HasDefaultValue(true);

        // Room Pricing entity config
        modelBuilder.Entity<RoomPricing>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<RoomPricing>()
            .Property(u => u.AdditionalPrice)
            .HasDefaultValue(0);

        // Duration Price entity config
        modelBuilder.Entity<DurationPrice>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<DurationPrice>()
            .Property(u => u.PriceMultiplier)
            .HasDefaultValue(1.00);

        // Bookings entity config
        modelBuilder.Entity<Bookings>()
            .Property(b => b.Status)
            .HasDefaultValue(BookingStatus.Pending);
        modelBuilder.Entity<Bookings>()
            .Property(b => b.PaymentStatus)
            .HasDefaultValue(PaymentStatus.Pending);
        modelBuilder.Entity<Bookings>()
            .Property(b => b.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Bookings>()
            .Property(b => b.UpdatedAt)
            .HasDefaultValue(DateTime.Now);

        // Booking Details entity config
        modelBuilder.Entity<BookingDetails>()
            .Property(b => b.BedroomCount)
            .HasDefaultValue(0);
        modelBuilder.Entity<BookingDetails>()
            .Property(b => b.BathroomCount)
            .HasDefaultValue(0);
        modelBuilder.Entity<BookingDetails>()
            .Property(b => b.KitchenCount)
            .HasDefaultValue(0);
        modelBuilder.Entity<BookingDetails>()
            .Property(b => b.LivingRoomCount)
            .HasDefaultValue(0);
        modelBuilder.Entity<BookingDetails>()
            .Property(b => b.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        
        modelBuilder.Entity<BookingDetails>()
            .HasOne(h => h.Booking)
            .WithOne(u => u.BookingDetails)
            .HasForeignKey<BookingDetails>(h => h.BookingId);

        // Contracts entity config
        modelBuilder.Entity<Contracts>()
            .Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.Now);

        // Complaints entity config
        modelBuilder.Entity<Complaints>()
            .Property(c => c.Status)
            .HasDefaultValue(ComplaintStatus.Pending);
        modelBuilder.Entity<Complaints>()
            .Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.Now);

        // BlacklistedUsers entity config
        modelBuilder.Entity<BlacklistedUsers>()
            .Property(c => c.BlacklistedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<BlacklistedUsers>()
            .Property(c => c.IsPermanent)
            .HasDefaultValue(false);

        // Notifications entity config
        modelBuilder.Entity<Notifications>()
            .Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Notifications>()
            .Property(c => c.IsRead)
            .HasDefaultValue(false);
    }

    public override int SaveChanges()
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entity in modifiedEntities)
        {
            entity.Property("UpdatedAt").CurrentValue = DateTime.Now;
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entity in modifiedEntities)
        {
            entity.Property("UpdatedAt").CurrentValue = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}