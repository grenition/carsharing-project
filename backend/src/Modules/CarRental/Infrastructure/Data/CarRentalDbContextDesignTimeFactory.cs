using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharedFramework.Database;

namespace CarRental.Infrastructure.Data;

public class CarRentalDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
{
    public CarRentalDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<CarRentalDbContext>();
        builder.AddSqlite();

        return new CarRentalDbContext(builder.Options);
    }
}