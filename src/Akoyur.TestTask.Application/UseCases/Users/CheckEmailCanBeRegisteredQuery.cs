using Akoyur.TestTask.ApiModels.Responses.Users;
using Akoyur.TestTask.Infrastructure.Database.Users;
using MediatR;

namespace Akoyur.TestTask.Application.UseCases.Users;

/// <summary>
/// Request for checking if an email can be registered.
/// </summary>
public record CheckEmailCanBeRegisteredQuery(string Email)
    : IRequest<CheckEmailCanBeRegisteredResponse>;

public class CheckEmailCanBeRegisteredQueryHandler(IMediator mediator)
    : IRequestHandler<CheckEmailCanBeRegisteredQuery, CheckEmailCanBeRegisteredResponse>
{
    /// <summary>
    /// Handles the CheckEmailCanBeRegisteredQuery and returns a response indicating whether the email can be registered.
    /// </summary>
    /// <param name="query">The query requesting the email registration check.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a CheckEmailCanBeRegisteredResponse as the result.</returns>
    public async Task<CheckEmailCanBeRegisteredResponse> Handle(CheckEmailCanBeRegisteredQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        // Sending the email check query to the database
        var canBeRegistered = await mediator.Send(new CheckEmailCanBeRegisteredDbQuery(query.Email));

        // Returning the response with the result of the email check
        return new CheckEmailCanBeRegisteredResponse(canBeRegistered);
    }
}
