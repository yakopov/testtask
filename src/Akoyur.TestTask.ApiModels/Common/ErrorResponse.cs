namespace Akoyur.TestTask.ApiModels.Common;

/// <summary>
/// Represents an error response that can include a message and an optional dictionary of validation errors.
/// </summary>
public record ErrorResponse(string? Message, Dictionary<string, string>? Errors = null) { }

