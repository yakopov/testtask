namespace Akoyur.TestTask.ApiModels.Requests.Users;

/// <summary>
/// Represents a request to create a new user with the specified email, password, and province ID.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the ID of the province the user is associated with.
    /// </summary>
    public int ProvinceId { get; set; }
}
