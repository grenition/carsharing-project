using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Configurations;

public class RentalModelConfiguration : IEntityTypeConfiguration<RentalModel>
{
    public void Configure(EntityTypeBuilder<RentalModel> builder)
    {
        builder.HasKey(r => r.Id);

        builder.OwnsOne(r => r.StartLocation);
        builder.OwnsOne(r => r.EndLocation);
        builder.OwnsOne(r => r.Price);

        builder.Property(r => r.Status).HasConversion<string>();

        builder.HasOne(r => r.UserModel)
            .WithMany(u => u.Rentals)
            .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.CarModel)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CarId);
    }
}