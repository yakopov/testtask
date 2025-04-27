using Akoyur.TestTask.ApiModels.Requests.Users;
using FluentValidation;

namespace Akoyur.TestTask.ApiModels.Validators;

/// <summary>
/// Validator for the <see cref="CreateUserRequest"/> class.
/// Ensures that the email is not empty, has a valid format, and meets length constraints.
/// Ensures that the password is not empty and does not exceed 30 characters.
/// Validates that the ProvinceId is greater than 0.
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(3).WithMessage("Email must be at least 3 characters long.")
            .MaximumLength(400).WithMessage("Email must not exceed 400 characters.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(x => x.ProvinceId)
            .GreaterThan(0).WithMessage("Invalid province identifier.");
    }
}
