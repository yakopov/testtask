using Akoyur.TestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace Akoyur.TestTask.Database;

/// <summary>
/// Database context for the application.
/// </summary>
public class TestTaskDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the TestTaskDbContext class.
    /// </summary>
    /// <param name="options">The context options.</param>
    public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options) { }

    /// <summary>
    /// Users table.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Countries table.
    /// </summary>
    public DbSet<Country> Countries { get; set; }

    /// <summary>
    /// Provinces table.
    /// </summary>
    public DbSet<Province> Provinces { get; set; }

    /// <summary>
    /// User profiles table.
    /// </summary>
    public DbSet<UserProfile> UserProfiles { get; set; }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entities
        ConfigureUsers(modelBuilder);
        ConfigureCountries(modelBuilder);
        ConfigureProvinces(modelBuilder);
        ConfigureUserProfiles(modelBuilder);

        // Seed initial data
        TestTaskDbSeeder.Seed(modelBuilder);
    }

    /// <summary>
    /// Configures the Country entity.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureCountries(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Country>()
            .HasMany(c => c.Provinces)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId);
    }

    /// <summary>
    /// Configures the Province entity.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureProvinces(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Province>()
            .HasKey(p => p.Id);
    }

    /// <summary>
    /// Configures the User entity.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserProfile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId);
    }

    /// <summary>
    /// Configures the UserProfile entity.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureUserProfiles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>()
            .HasKey(x => x.UserId);

        modelBuilder.Entity<UserProfile>()
            .HasOne(p => p.Province)
            .WithMany()
            .HasForeignKey(p => p.ProvinceId);
    }
}
