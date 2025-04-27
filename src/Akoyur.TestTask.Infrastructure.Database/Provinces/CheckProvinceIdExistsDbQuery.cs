using Akoyur.TestTask.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akoyur.TestTask.Infrastructure.Database.Province;

/// <summary>
/// Query for checking if a province with the specified ID exists in the database.
/// </summary>
public record CheckProvinceIdExistsDbQuery(int ProvinceId) : IRequest<bool>;

/// <summary>
/// Handler for <see cref="CheckProvinceIdExistsDbQuery"/> that checks if a province exists in the database.
/// </summary>
public class CheckProvinceIdExistsDbQueryHandler(TestTaskDbContext db) : IRequestHandler<CheckProvinceIdExistsDbQuery, bool>
{
    /// <summary>
    /// Handles the <see cref="CheckProvinceIdExistsDbQuery"/> by checking if a province with the given ID exists.
    /// </summary>
    /// <param name="request">The query request containing the province ID.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>True if the province exists and is not deleted, otherwise false.</returns>
    public async Task<bool> Handle(CheckProvinceIdExistsDbQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Check if the province exists and is not deleted
        return await db.Provinces.AnyAsync(x => !x.IsDeleted && x.Id == request.ProvinceId, cancellationToken);
    }
}
