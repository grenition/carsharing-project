using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Configurations;

public class RentalModelConfiguration : IEntityTypeConfiguration<RentalModel>
{
    public void Configure(EntityTypeBuilder<RentalModel> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CarId);
        builder.Property(r => r.Status).HasConversion<string>();

        builder.OwnsOne(r => r.StartLocation);
        builder.OwnsOne(r => r.EndLocation);
        builder.OwnsOne(r => r.Price);

    }
}
