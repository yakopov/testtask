using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Akoyur.TestTask.Database.DI;

/// <summary>
/// Dependency injection helper class for registering database services.
/// </summary>
public static class DI
{
    /// <summary>
    /// Adds the database services to the IServiceCollection, including the DbContext configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The updated IServiceCollection with the database services added.</returns>
    public static IServiceCollection AddSqliteDatabase(this IServiceCollection services, string sqLiteDirectory)
    {
        // Configure the DbContext to use SQLite with a specified database file
        var dbPath = Path.Combine(sqLiteDirectory.Replace("{BASE_PATH}", AppDomain.CurrentDomain.BaseDirectory), "app.db");
        services
            .AddDbContext<TestTaskDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"))
            .BuildServiceProvider();

        // Register the DbContext as a scoped service
        services.AddScoped<DbContext, TestTaskDbContext>();

        return services;
    }
}
