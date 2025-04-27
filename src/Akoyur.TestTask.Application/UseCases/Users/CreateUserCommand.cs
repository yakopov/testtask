using Akoyur.TestTask.ApiModels.Responses.Users;
using Akoyur.TestTask.Helpers;
using Akoyur.TestTask.Infrastructure.Database.Province;
using Akoyur.TestTask.Infrastructure.Database.Users;
using MediatR;

namespace Akoyur.TestTask.Application.UseCases.Users;

/// <summary>
/// Command for creating a new user.
/// </summary>
public record CreateUserCommand(string Email, string Password, int ProvinceId)
    : IRequest<CreateUserResponse>;

public class CreateUserCommandHandler(IMediator mediator)
    : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    /// <summary>
    /// Handles the CreateUserCommand and creates a new user after validating the provided data.
    /// </summary>
    /// <param name="command">The command containing the user creation data.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a CreateUserResponse as the result.</returns>
    public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        // Check if the provided province exists
        var provinceExists = await mediator.Send(new CheckProvinceIdExistsDbQuery(command.ProvinceId));
        if (!provinceExists)
        {
            ExceptionHelper.ThrowValidationException(
                nameof(command.ProvinceId),
                "Invalid province identifier.");
        }

        // Check if the provided email can be registered
        var emailCanBeRegistered = await mediator.Send(new CheckEmailCanBeRegisteredDbQuery(command.Email));
        if (!emailCanBeRegistered)
        {
            ExceptionHelper.ThrowValidationException(
                nameof(command.Email),
                "Email is already taken.");
        }

        // Create a hash for the provided password
        var passwordHash = PasswordHelper.CreatePasswordHash(command.Password);

        // Create the database command to insert the new user
        var dbCommand = new CreateUserDbCommand(command.Email, passwordHash, command.ProvinceId);
        var userId = await mediator.Send(dbCommand);

        // Return the response containing the created user ID
        return new CreateUserResponse(userId);
    }
}
