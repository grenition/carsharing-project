using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Configurations;

public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.VIN).IsRequired();
        builder.Property(c => c.LicensePlate).IsRequired();
        builder.Property(c => c.Manufacturer).IsRequired();
        builder.Property(c => c.Model).IsRequired();

        builder.OwnsOne(c => c.Location);

        builder.Property(c => c.Status).HasConversion<string>();
        builder.Property(c => c.FuelLevel);
        builder.Property(c => c.CurrentMileage);
        builder.Property(c => c.LastServiceDate);
    }
}