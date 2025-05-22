using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Configurations;

public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.DriverLicense, dl =>
        {
            dl.Property(p => p.Number).HasColumnName("DriverLicenseNumber");
            dl.Property(p => p.IssuedDate).HasColumnName("DriverLicenseIssuedDate");
            dl.Property(p => p.ExpirationDate).HasColumnName("DriverLicenseExpirationDate");
        });

        builder.Property(u => u.FullName).IsRequired();
        builder.Property(u => u.PhoneNumber).IsRequired();
        builder.Property(u => u.Balance).HasColumnType("decimal(18,2)");
        builder.Property(u => u.Status).HasConversion<string>();
    }
}