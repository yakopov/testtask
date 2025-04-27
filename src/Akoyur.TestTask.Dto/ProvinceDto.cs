namespace Akoyur.TestTask.Dto;

/// <summary>
/// DTO representing a province.
/// </summary>
public class ProvinceDto
{
    /// <summary>
    /// Province's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifier of the associated country.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Province's name.
    /// </summary>
    public string Name { get; set; } = null!;
}

