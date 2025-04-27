using Akoyur.TestTask.Database;
using Akoyur.TestTask.Entities;
using MediatR;

namespace Akoyur.TestTask.Infrastructure.Database.Users;

/// <summary>
/// Command to create a new user in the database.
/// </summary>
public record CreateUserDbCommand(string Email, string PasswordHash, int ProvinceId) : IRequest<int>;

/// <summary>
/// Handler for <see cref="CreateUserDbCommand"/> that creates a new user in the database.
/// </summary>
public class CreateUserDbCommandHandler(TestTaskDbContext db) : IRequestHandler<CreateUserDbCommand, int>
{
    /// <summary>
    /// Handles the command to create a user and save the data in the database.
    /// </summary>
    /// <param name="command">The command containing user details.</param>
    /// <param name="cancellationToken">Token for cancellation of the operation.</param>
    /// <returns>The ID of the newly created user.</returns>
    public async Task<int> Handle(CreateUserDbCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var user = new User
        {
            CreatedAtUtc = DateTime.UtcNow,
            Email = command.Email,
            PasswordHash = command.PasswordHash,

            UserProfile = new UserProfile
            {
                ProvinceId = command.ProvinceId
            }
        };

        await db.Users.AddAsync(user, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        // Return the ID of the newly created user
        return user.Id;
    }
}