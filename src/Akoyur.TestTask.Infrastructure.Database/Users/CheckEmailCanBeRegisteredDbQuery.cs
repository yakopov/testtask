using Akoyur.TestTask.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akoyur.TestTask.Infrastructure.Database.Users;

/// <summary>
/// Query to check if the email can be registered (i.e., it is not already taken).
/// </summary>
public record CheckEmailCanBeRegisteredDbQuery(string Email) : IRequest<bool>;

/// <summary>
/// Handler for <see cref="CheckEmailCanBeRegisteredDbQuery"/> that checks if the provided email is already in use.
/// </summary>
public class CheckEmailCanBeRegisteredDbQueryHandler(TestTaskDbContext db) : IRequestHandler<CheckEmailCanBeRegisteredDbQuery, bool>
{
    /// <summary>
    /// Handles the query to check if the email is already registered in the database.
    /// </summary>
    /// <param name="query">The query containing the email to check.</param>
    /// <param name="cancellationToken">Token for cancellation of the operation.</param>
    /// <returns>True if the email can be registered, false if it is already taken.</returns>
    public async Task<bool> Handle(CheckEmailCanBeRegisteredDbQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        // Check if the email already exists in the database
        return !await db.Users.AnyAsync(x => x.Email == query.Email);
    }
}