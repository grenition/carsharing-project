using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharedFramework.Database;

namespace Users.Infrastructure.Data;

public class UsersDbContextDesignTimeFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<UsersDbContext>();
        builder.AddSqlite();

        return new UsersDbContext(builder.Options);
    }
}
