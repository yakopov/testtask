using Akoyur.TestTask.ApiModels.Requests.Users;
using Akoyur.TestTask.ApiModels.Responses.Users;
using Akoyur.TestTask.Application.UseCases.Users;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Akoyur.TestTask.WebAPI.Controllers;

/// <summary>
/// Controller for managing user-related operations.
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v1/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new user with the provided details.
    /// </summary>
    /// <param name="request">The user creation request containing Email, Password, and ProvinceId.</param>
    /// <returns>The response with the newly created user's ID.</returns>
    [HttpPost]
    [Route("")]
    public Task<CreateUserResponse> CreateUser([FromBody] CreateUserRequest request)
        => mediator.Send(new CreateUserCommand(request.Email!, request.Password!, request.ProvinceId));

    /// <summary>
    /// Checks whether the provided email can be registered (i.e., it is not already taken).
    /// </summary>
    /// <param name="request">The request containing the email to be checked.</param>
    /// <returns>Response indicating if the email is available for registration.</returns>
    [HttpGet]
    [Route("check-email")]
    public Task<CheckEmailCanBeRegisteredResponse> CheckEmail([FromQuery] CheckEmailCanBeRegisteredRequest request)
        => mediator.Send(new CheckEmailCanBeRegisteredQuery(request.Email!));
}
