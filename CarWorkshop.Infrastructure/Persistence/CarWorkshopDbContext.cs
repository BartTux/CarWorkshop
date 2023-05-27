using CarWorkshop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence;

public class CarWorkshopDbContext : IdentityDbContext
{
    public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
    public DbSet<CarWorkshopService> Services { get; set; }

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
    }
}
