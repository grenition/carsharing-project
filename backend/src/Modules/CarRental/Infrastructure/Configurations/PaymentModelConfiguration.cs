using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Configurations;

public class PaymentModelConfiguration : IEntityTypeConfiguration<PaymentModel>
{
    public void Configure(EntityTypeBuilder<PaymentModel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.RentalId);
        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Type).HasConversion<string>();
        builder.Property(p => p.Status).HasConversion<string>();
        builder.Property(p => p.Timestamp);
    }
}
