using CarWorkshop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence;

public class CarWorkshopDbContext : IdentityDbContext
{
    public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
    public DbSet<CarWorkshopService> Services { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CarWorkshopServiceCart> ServiceCarts { get; set; }

    public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Domain.Entities.CarWorkshop>()
            .OwnsOne(x => x.ContactDetails);

        modelBuilder
            .Entity<Domain.Entities.CarWorkshop>()
            .HasMany(x => x.Services)
            .WithOne(x => x.CarWorkshop)
            .HasForeignKey(x => x.CarWorkshopId);

        modelBuilder
            .Entity<CarWorkshopService>()
            .Property(x => x.Cost)
            .HasPrecision(18, 2);

        modelBuilder
            .Entity<CarWorkshopServiceCart>()
            .HasKey(c => new { c.CartId, c.CarWorkshopServiceId });

        modelBuilder
            .Entity<CarWorkshopServiceCart>()
            .HasOne(c => c.Cart)
            .WithMany(c => c.ServiceCarts)
            .HasForeignKey(c => c.CartId);

        modelBuilder
            .Entity<CarWorkshopServiceCart>()
            .HasOne(c => c.CarWorkshopService)
            .WithMany(c => c.ServiceCarts)
            .HasForeignKey(c => c.CarWorkshopServiceId);

        modelBuilder
            .Entity<CarWorkshopServiceCart>()
            .Property(c => c.Quantity)
            .IsRequired();
    }
}
