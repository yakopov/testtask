using FluentValidation;
using FluentValidation.Results;

namespace Akoyur.TestTask.Helpers;

/// <summary>
/// Helper class for exception handling.
/// </summary>
public static class ExceptionHelper
{
    /// <summary>
    /// Throws a validation exception with a specific property name and error message.
    /// </summary>
    /// <param name="propertyName">The name of the property that caused the validation error.</param>
    /// <param name="errorMessage">The error message describing the validation issue.</param>
    /// <exception cref="ValidationException">Thrown when the validation fails.</exception>
    public static void ThrowValidationException(string propertyName, string errorMessage)
        => throw new ValidationException(new[] { new ValidationFailure(propertyName, errorMessage) });
}
