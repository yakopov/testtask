using Akoyur.TestTask.ApiModels.Responses.Countries;
using Akoyur.TestTask.Enumerations;
using Akoyur.TestTask.Infrastructure.Database.Countries;
using MediatR;

namespace Akoyur.TestTask.Application.UseCases.Countries;

/// <summary>
/// Request for retrieving all countries.
/// </summary>
public record GetAllCountriesQuery()
    : IRequest<GetAllCountriesResponse>;

public class GetAllCountriesQueryHandler(IMediator mediator)
    : IRequestHandler<GetAllCountriesQuery, GetAllCountriesResponse>
{
    /// <summary>
    /// Handles the GetAllCountriesQuery and returns a response containing all countries.
    /// </summary>
    /// <param name="request">The query requesting all countries.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a GetAllCountriesResponse as the result.</returns>
    public async Task<GetAllCountriesResponse> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Creating a query for fetching countries from the database with sorting by name in ascending order
        var query = new GetCountriesDbQuery(SortOrder.NameAsc);
        var countries = await mediator.Send(query);

        // Returning the response with the list of countries
        return new GetAllCountriesResponse(countries);
    }
}