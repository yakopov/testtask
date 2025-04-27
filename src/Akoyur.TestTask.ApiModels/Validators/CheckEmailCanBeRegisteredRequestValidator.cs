using Akoyur.TestTask.ApiModels.Responses.Users;
using FluentValidation;

namespace Akoyur.TestTask.ApiModels.Validators;

/// <summary>
/// Validator for the <see cref="CheckEmailCanBeRegisteredRequest"/> class.
/// Ensures the email is not empty, has a valid format, and meets length constraints.
/// </summary>
public class CheckEmailCanBeRegisteredRequestValidator : AbstractValidator<CheckEmailCanBeRegisteredRequest>
{
    public CheckEmailCanBeRegisteredRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(3).WithMessage("Email must be at least 3 characters long.")
            .MaximumLength(400).WithMessage("Email must not exceed 400 characters.")
            .EmailAddress().WithMessage("Email format is invalid.");
    }
}
