namespace Akoyur.TestTask.Dto;

/// <summary>
/// DTO representing a country.
/// </summary>
public class CountryDto
{
    /// <summary>
    /// Country's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Country's name.
    /// </summary>
    public string Name { get; set; } = null!;
}
