using Akoyur.TestTask.Abstractions.Database;
using Akoyur.TestTask.Enumerations;

namespace Akoyur.TestTask.Database.Extensions;

/// <summary>
/// Extension methods for IQueryable to apply sorting based on a SortOrder.
/// </summary>
public static class IQueriableExtensions
{
    /// <summary>
    /// Applies sorting to the IQueryable based on the specified SortOrder.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the IQueryable, which must implement ISortableEntity.</typeparam>
    /// <param name="queryable">The IQueryable to apply sorting to.</param>
    /// <param name="sortOrder">The SortOrder specifying the sorting criteria.</param>
    /// <returns>An IOrderedQueryable<T> with the applied sorting.</returns>
    /// <exception cref="NotSupportedException">Thrown when the specified SortOrder is not supported.</exception>
    public static IOrderedQueryable<T> ApplySorting<T>(this IQueryable<T> queryable, SortOrder sortOrder) where T : ISortableEntity
        => sortOrder switch
        {
            SortOrder.IdAsc => queryable.OrderBy(x => x.Id),
            SortOrder.IdDesc => queryable.OrderByDescending(x => x.Id),
            SortOrder.NameAsc => queryable.OrderBy(x => x.Name),
            SortOrder.NameDesc => queryable.OrderByDescending(x => x.Name),
            _ => throw new NotSupportedException($"The specified sort type {sortOrder} is not supported"),
        };
}
