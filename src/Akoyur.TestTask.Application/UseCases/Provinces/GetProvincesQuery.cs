using Akoyur.TestTask.ApiModels.Responses.Provinces;
using Akoyur.TestTask.Enumerations;
using Akoyur.TestTask.Infrastructure.Database.Province;
using MediatR;

namespace Akoyur.TestTask.Application.UseCases.Countries.GetProvinces;

/// <summary>
/// Request for retrieving provinces by country.
/// </summary>
public record GetProvincesQuery(int? CountryId)
    : IRequest<GetProvincesResponse>;

public class GetProvincesQueryHandler(IMediator mediator)
    : IRequestHandler<GetProvincesQuery, GetProvincesResponse>
{
    /// <summary>
    /// Handles the GetProvincesQuery and returns a response containing the provinces for the specified country.
    /// </summary>
    /// <param name="request">The query requesting the provinces.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a GetProvincesResponse as the result.</returns>
    public async Task<GetProvincesResponse> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Creating a query to fetch provinces from the database with sorting by name in ascending order
        var query = new GetProvincesDbQuery(request.CountryId, SortOrder.NameAsc);
        var provinces = await mediator.Send(query);

        // Returning the response with the list of provinces
        return new GetProvincesResponse(provinces);
    }
}
