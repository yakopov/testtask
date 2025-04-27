namespace Akoyur.TestTask.ApiModels.Responses.Users;

/// <summary>
/// Represents a request to check if the provided email can be registered.
/// </summary>
public record CheckEmailCanBeRegisteredRequest(string Email) { }

