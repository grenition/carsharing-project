using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFramework.Database;

public static class DbExtensions
{
    internal static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<DbContextInitializer>();
        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<T>(x => x.UseNpgsql(connectionString));
        return services;
    }

    public static DbContextOptionsBuilder AddPostgres(this DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("Postgres");
        return optionsBuilder.UseNpgsql(connectionString);
    }
    
    internal static IServiceCollection AddSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<DbContextInitializer>();
        return services;
    }

    public static IServiceCollection AddSqlite<T>(this IServiceCollection services) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Sqlite");

        services.AddDbContext<T>(options => options.UseSqlite(connectionString));
        return services;
    }

    public static DbContextOptionsBuilder AddSqlite(this DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("Sqlite");
        return optionsBuilder.UseSqlite(connectionString);
    }
}