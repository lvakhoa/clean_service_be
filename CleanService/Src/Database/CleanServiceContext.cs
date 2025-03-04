using CleanService.Src.Models.Configurations;
using CleanService.Src.Models.Enums;

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

    public virtual DbSet<HelperAvailability> HelperAvailability { get; set; } = null!;

    public virtual DbSet<ServiceCategories> ServiceCategory { get; set; } = null!;

    public virtual DbSet<ServiceTypes> ServiceTypes { get; set; } = null!;

    public virtual DbSet<RoomPricing> RoomPricing { get; set; } = null!;

    public virtual DbSet<DurationPrice> DurationPrice { get; set; } = null!;

    public virtual DbSet<Bookings> Bookings { get; set; } = null!;

    public virtual DbSet<BookingDetails> BookingDetails { get; set; } = null!;

    public virtual DbSet<BookingContracts> BookingContracts { get; set; } = null!;

    public virtual DbSet<Contracts> Contracts { get; set; } = null!;

    public virtual DbSet<Feedbacks> Feedbacks { get; set; } = null!;

    public virtual DbSet<Refunds> Refunds { get; set; } = null!;

    public virtual DbSet<BlacklistedUsers> BlacklistedUsers { get; set; } = null!;

    public virtual DbSet<Notifications> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users entity config
        modelBuilder.Entity<Users>()
            .Navigation(u => u.Helper)
            .AutoInclude();
        modelBuilder.Entity<Users>()
            .Navigation(u => u.BlacklistedUsers)
            .AutoInclude();
        modelBuilder.Entity<Users>()
            .Navigation(u => u.BlacklistedByUsers)
            .AutoInclude();

        modelBuilder.Entity<Users>()
            .Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Users>()
            .Property(u => u.UpdatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Users>()
            .Property(u => u.Status)
            .HasDefaultValue(UserStatus.Active);
        modelBuilder.Entity<Users>()
            .Property(u => u.NumberOfViolation)
            .HasDefaultValue(0);

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

        modelBuilder.Entity<Helpers>()
            .Navigation(h => h.User)
            .AutoInclude();

        // Helper Availability entity config
        modelBuilder.Entity<HelperAvailability>()
            .Property(ha => ha.Status)
            .HasDefaultValue(AvailabilityStatus.Pending);
        modelBuilder.Entity<HelperAvailability>()
            .Property(ha => ha.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<HelperAvailability>()
            .Property(ha => ha.UpdatedAt)
            .HasDefaultValue(DateTime.Now);

        // Service Categories entity config
        // modelBuilder.Entity<ServiceCategories>()
        //     .Navigation(s => s.ServiceTypes)
        //     .AutoInclude();

        modelBuilder.Entity<ServiceCategories>()
            .Property(s => s.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<ServiceCategories>()
            .Property(s => s.IsActive)
            .HasDefaultValue(true);

        // Service Types entity config
        modelBuilder.Entity<ServiceTypes>()
            .Navigation(s => s.RoomPricing)
            .AutoInclude();
        modelBuilder.Entity<ServiceTypes>()
            .Navigation(s => s.DurationPrice)
            .AutoInclude();
        modelBuilder.Entity<ServiceTypes>()
            .Navigation(s => s.Category)
            .AutoInclude();

        modelBuilder.Entity<ServiceTypes>()
            .Property(s => s.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<ServiceTypes>()
            .Property(s => s.IsActive)
            .HasDefaultValue(true);

        // Room Pricing entity config
        modelBuilder.Entity<RoomPricing>()
            .Property(r => r.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<RoomPricing>()
            .Property(r => r.AdditionalPrice)
            .HasDefaultValue(0);
        modelBuilder.Entity<RoomPricing>()
            .Navigation(s => s.ServiceType)
            .AutoInclude();

        // Duration Price entity config
        modelBuilder.Entity<DurationPrice>()
            .Property(d => d.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<DurationPrice>()
            .Property(d => d.PriceMultiplier)
            .HasDefaultValue(1.00);
        modelBuilder.Entity<DurationPrice>()
            .Navigation(d => d.ServiceType)
            .AutoInclude();

        // Bookings entity config
        modelBuilder.Entity<Bookings>()
            .Navigation(b => b.Customer)
            .AutoInclude();
        modelBuilder.Entity<Bookings>()
            .Navigation(b => b.Helper)
            .AutoInclude();
        modelBuilder.Entity<Bookings>()
            .Navigation(b => b.ServiceType)
            .AutoInclude();
        modelBuilder.Entity<Bookings>()
            .Navigation(b => b.Contract)
            .AutoInclude();
        modelBuilder.Entity<Bookings>()
            .Navigation(b => b.BookingDetails)
            .AutoInclude();

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
            .Navigation(b => b.DurationPrice)
            .AutoInclude();

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
            .HasOne(bd => bd.Booking)
            .WithOne(b => b.BookingDetails)
            .HasForeignKey<BookingDetails>(bd => bd.BookingId);

        // Booking Contracts entity config
        modelBuilder.Entity<BookingContracts>()
            .Property(bc => bc.CreatedAt)
            .HasDefaultValue(DateTime.Now);

        // Contracts entity config
        modelBuilder.Entity<Contracts>()
            .Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Contracts>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValue(DateTime.Now);

        // Feedback entity config
        modelBuilder.Entity<Feedbacks>()
            .Navigation(b => b.Booking)
            .AutoInclude();

        modelBuilder.Entity<Feedbacks>()
            .Property(f => f.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Feedbacks>()
            .Property(f => f.UpdatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Feedbacks>()
            .Navigation(b => b.Booking)
            .AutoInclude();

        // Refunds entity config
        modelBuilder.Entity<Refunds>()
            .Navigation(b => b.Booking)
            .AutoInclude();

        modelBuilder.Entity<Refunds>()
            .Property(r => r.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Refunds>()
            .Property(r => r.Status)
            .HasDefaultValue(RefundStatus.Pending);
        modelBuilder.Entity<Refunds>()
            .Navigation(b => b.Booking)
            .AutoInclude();


        // BlacklistedUsers entity config
        modelBuilder.Entity<BlacklistedUsers>()
            .Property(b => b.BlacklistedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<BlacklistedUsers>()
            .Property(b => b.IsPermanent)
            .HasDefaultValue(false);

        // Notifications entity config
        modelBuilder.Entity<Notifications>()
            .Property(n => n.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Notifications>()
            .Property(n => n.IsRead)
            .HasDefaultValue(false);
    }

    public override int SaveChanges()
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entity in modifiedEntities)
        {
            if (entity.GetType().GetProperty("UpdatedAt") != null)
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
            if (entity.GetType().GetProperty("UpdatedAt") != null)
                entity.Property("UpdatedAt").CurrentValue = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}
