using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure;

public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<CarModel> Cars => Set<CarModel>();
    public DbSet<RentalModel> Rentals => Set<RentalModel>();
    public DbSet<PaymentModel> Payments => Set<PaymentModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
    }
}