using Akoyur.TestTask.Abstractions.Database;

namespace Akoyur.TestTask.Entities;

/// <summary>
/// Represents a province entity with sorting capabilities and basic properties.
/// </summary>
public class Province : ISortableEntity
{
    /// <summary>
    /// Unique identifier for the province.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key referencing the country the province belongs to.
    /// </summary>
    public int CountryId { get; set; }

    // Technical properties

    /// <summary>
    /// Indicates whether the province is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The UTC timestamp of when the province was deleted.
    /// </summary>
    public DateTime? DeletedAtUtc { get; set; }

    // Main properties

    /// <summary>
    /// The name of the province.
    /// </summary>
    public string Name { get; set; } = null!;

    // Navigation properties

    /// <summary>
    /// The country to which the province belongs.
    /// </summary>
    public Country Country { get; set; } = null!;
}
