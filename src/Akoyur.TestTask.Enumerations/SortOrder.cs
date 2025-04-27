namespace Akoyur.TestTask.Enumerations;

/// <summary>
/// Enum representing the different sort orders that can be applied to queries.
/// </summary>
public enum SortOrder
{
    /// <summary>
    /// Sort by ID in ascending order.
    /// </summary>
    IdAsc = 1,

    /// <summary>
    /// Sort by ID in descending order.
    /// </summary>
    IdDesc = 2,

    /// <summary>
    /// Sort by Name in ascending order (alphabetically A-Z).
    /// </summary>
    NameAsc = 3,

    /// <summary>
    /// Sort by Name in descending order (alphabetically Z-A).
    /// </summary>
    NameDesc = 4
}