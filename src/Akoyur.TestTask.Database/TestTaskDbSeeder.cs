using Akoyur.TestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace Akoyur.TestTask.Database;

/// <summary>
/// Seeder class for populating the database with initial data.
/// </summary>
public static class TestTaskDbSeeder
{
    /// <summary>
    /// Seeds the database with initial data for Countries and Provinces.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to configure the model.</param>
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed data for Countries
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Country 1" },
            new Country { Id = 2, Name = "Country 2" }
        );

        // Seed data for Provinces
        modelBuilder.Entity<Province>().HasData(
            new Province { Id = 1, Name = "Province 1.1", CountryId = 1 },
            new Province { Id = 2, Name = "Province 1.2", CountryId = 1 },
            new Province { Id = 3, Name = "Province 2.1", CountryId = 2 }
        );
    }
}