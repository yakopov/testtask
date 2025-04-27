using Akoyur.TestTask.Database;
using Akoyur.TestTask.Database.Extensions;
using Akoyur.TestTask.Dto;
using Akoyur.TestTask.Enumerations;
using Akoyur.TestTask.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akoyur.TestTask.Infrastructure.Database.Province;

/// <summary>
/// Query for retrieving provinces, optionally filtered by country ID and sorted by a specific order.
/// </summary>
public record GetProvincesDbQuery(int? CountryId, SortOrder SortOrder) : IRequest<IReadOnlyCollection<ProvinceDto>>;

/// <summary>
/// Handler for <see cref="GetProvincesDbQuery"/> that retrieves a list of provinces from the database.
/// </summary>
public class GetProvincesDbQueryHandler(TestTaskDbContext db) : IRequestHandler<GetProvincesDbQuery, IReadOnlyCollection<ProvinceDto>>
{
    /// <summary>
    /// Handles the <see cref="GetProvincesDbQuery"/> by fetching provinces, applying optional filtering by country, 
    /// and sorting according to the specified <see cref="SortOrder"/>.
    /// </summary>
    /// <param name="request">The query request containing the optional country ID and sort order.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="ProvinceDto"/> representing the provinces.</returns>
    public async Task<IReadOnlyCollection<ProvinceDto>> Handle(GetProvincesDbQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = db.Provinces
            .Where(x => !x.IsDeleted);

        // If a CountryId is provided, filter by it
        if (request.CountryId.HasValue)
        {
            query = query.Where(x => x.CountryId == request.CountryId);
        }

        // Apply sorting and project to DTOs
        var querySelector = query
            .ApplySorting(request.SortOrder)
            .Select(x => x.ToDto());

        // Return the list of provinces as DTOs
        return await querySelector.ToListAsync(cancellationToken);
    }
}