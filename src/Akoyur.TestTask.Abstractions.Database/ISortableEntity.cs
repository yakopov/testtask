namespace Akoyur.TestTask.Abstractions.Database;

/// <summary>
/// Interface for entities that can be sorted by their ID and Name properties.
/// </summary>
public interface ISortableEntity
{
    /// <summary>
    /// Gets or sets the ID of the entity.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the entity.
    /// </summary>
    string Name { get; set; }
}
