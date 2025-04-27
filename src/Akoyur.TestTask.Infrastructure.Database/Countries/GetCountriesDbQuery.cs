using Akoyur.TestTask.Database;
using Akoyur.TestTask.Database.Extensions;
using Akoyur.TestTask.Dto;
using Akoyur.TestTask.Enumerations;
using Akoyur.TestTask.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akoyur.TestTask.Infrastructure.Database.Countries;

/// <summary>
/// Query for retrieving countries from the database with sorting options.
/// </summary>
public record GetCountriesDbQuery(SortOrder SortOrder) : IRequest<IReadOnlyCollection<CountryDto>>;

/// <summary>
/// Handler for <see cref="GetCountriesDbQuery"/> that retrieves countries from the database.
/// </summary>
public class GetCountriesDbQueryHandler(TestTaskDbContext db) : IRequestHandler<GetCountriesDbQuery, IReadOnlyCollection<CountryDto>>
{
    /// <summary>
    /// Handles the <see cref="GetCountriesDbQuery"/> by fetching countries from the database.
    /// </summary>
    /// <param name="request">The query request with sorting options.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A collection of country DTOs.</returns>
    public async Task<IReadOnlyCollection<CountryDto>> Handle(GetCountriesDbQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Build the query to retrieve countries, applying sorting and excluding deleted ones
        var query = db.Countries
            .Where(x => !x.IsDeleted)
            .ApplySorting(request.SortOrder)
            .Select(x => x.ToDto());

        return await query.ToListAsync(cancellationToken);
    }
}
