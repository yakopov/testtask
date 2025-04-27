using Akoyur.TestTask.Database.DI;
using Akoyur.TestTask.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Akoyur.TestTask.Application.DI;

/// <summary>
/// Dependency injection helper class for registering application services.
/// </summary>
public static class DI
{
    /// <summary>
    /// Adds the application services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The updated IServiceCollection with the application services added.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Register MediatR services from the current assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register MediatR services from the Infrastructure database assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InfrastructureDatabaseAssembly).Assembly));

        // Add database services to the collection
        var sqliteBasePath = configuration.GetConnectionString("SqliteBasePath")!;
        services.AddSqliteDatabase(sqliteBasePath);

        return services;
    }
}
