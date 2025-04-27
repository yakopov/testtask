namespace Akoyur.TestTask.ApiModels.Common;

/// <summary>
/// Represents a paginated collection response, including the list of items, a flag indicating whether there are more items,
/// and an optional cursor for pagination.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
public abstract class CollectionResponse<T>(IReadOnlyCollection<T> items, bool hasMore = false, int? cursor = null)
{
    /// <summary>
    /// Gets the collection of items in the response.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; init; } = items;

    /// <summary>
    /// Gets a value indicating whether there are more items available for pagination.
    /// </summary>
    public bool HasMore { get; init; } = hasMore;

    /// <summary>
    /// Gets the cursor to fetch the next set of items for pagination, if applicable.
    /// </summary>
    public int? Cursor { get; init; } = cursor;
}
