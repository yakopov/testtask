using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Akoyur.TestTask.Database;

/// <summary>
/// Factory class for creating an instance of TestTaskDbContext during design time.
/// </summary>
public class TestTaskDbContextFactory : IDesignTimeDbContextFactory<TestTaskDbContext>
{
    /// <summary>
    /// Creates a new instance of TestTaskDbContext with the specified options.
    /// </summary>
    /// <param name="args">Arguments for the DbContext creation.</param>
    /// <returns>A new instance of TestTaskDbContext.</returns>
    public TestTaskDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TestTaskDbContext>();
        optionsBuilder.UseSqlite($"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.db")}");

        // Return the created context with the configured options
        return new TestTaskDbContext(optionsBuilder.Options);
    }
}
