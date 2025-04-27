namespace Akoyur.TestTask.Entities;

/// <summary>
/// Represents a user entity in the system with essential user details and navigation to user profile.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The UTC timestamp when the user was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The hashed password of the user.
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    // Navigation properties

    /// <summary>
    /// The user's profile details.
    /// </summary>
    public UserProfile UserProfile { get; set; } = null!;
}

