namespace Akoyur.TestTask.Entities;


/// <summary>
/// Represents the user's profile containing additional information like the user's province.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Unique identifier for the user to whom the profile belongs.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The identifier for the province where the user resides.
    /// </summary>
    public int ProvinceId { get; set; }

    // Navigation properties

    /// <summary>
    /// The user associated with this profile.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// The province the user is associated with.
    /// </summary>
    public Province Province { get; set; } = null!;
}
