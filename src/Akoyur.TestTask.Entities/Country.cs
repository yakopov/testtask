using Akoyur.TestTask.Abstractions.Database;

namespace Akoyur.TestTask.Entities;

/// <summary>
/// Represents a country entity with sorting capabilities and basic properties.
/// </summary>
public class Country : ISortableEntity
{
    /// <summary>
    /// Unique identifier for the country.
    /// </summary>
    public int Id { get; set; }

    // Technical properties

    /// <summary>
    /// Indicates whether the country is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The UTC timestamp of when the country was deleted.
    /// </summary>
    public DateTime? DeletedAtUtc { get; set; }

    // Main properties

    /// <summary>
    /// The name of the country.
    /// </summary>
    public string Name { get; set; } = null!;

    // Navigation properties

    /// <summary>
    /// Collection of provinces associated with the country.
    /// </summary>
    public ICollection<Province> Provinces { get; set; } = [];
}
